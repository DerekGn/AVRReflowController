/**
* MIT License
*
* Copyright (c) 2016 Derek Goslin < http://corememorydump.blogspot.ie/ >
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

#include "main.h"

static volatile bool recalc_pid = 0;

typedef struct _ReflowContext {
	uint16_t profile_time;
	uint16_t reflow_timer;
	uint16_t target_temp;
	uint16_t tc_temp;
	uint8_t update;
} ReflowContext;

uint8_t buffer[CDC_TXRX_EPSIZE];

/** Contains the current baud rate and other settings of the virtual serial port. While this demo does not use
*  the physical USART and thus does not use these settings, they must still be retained and returned to the host
*  upon request or the host will assume the device is non-functional.
*
*  These values are set by the host via a class-specific request, however they are not required to be used accurately.
*  It is possible to completely ignore these value or use other settings as the host is completely unaware of the physical
*  serial link characteristics and instead sends and receives data in endpoint streams.
*/
static CDC_LineEncoding_t LineEncoding = { .BaudRateBPS = 0,
	.CharFormat  = CDC_LINEENCODING_OneStopBit,
	.ParityType  = CDC_PARITY_None,
.DataBits    = 8};

ReflowContext ctx = { 0, 0, 0, 0};
Response response;
Request request;

void Stop_Cycle()
{
	Set_Profile_Stop();
	Restore_Timer_Counter1();
	Set_Relay_State(false);
}

ProfileStage MapProfileStage(ProfileState profileState, ReflowContext ctx) {
	
	ProfileStage profileStage;

	profileStage.ReflowTimer = ctx.profile_time;
	profileStage.State = (ProfileStage_ProfileStageState)profileState;
	profileStage.TargetTemp = ctx.target_temp;
	profileStage.TcTemp = ctx.tc_temp;

	return profileStage;
}

void SetErrorResponse(Response * response, Response_ErrorCodeType errorCode) {
	
	response->which_payload = Response_ErrorCode_tag;
	response->Result = Response_ResultType_FAIL;
	response->payload.ErrorCode = errorCode;
}

static void Handle_Timer_Overflow() {
	recalc_pid = 1;
}

ReflowProfile MapFromProfile(Profile profile) {
	
	ReflowProfile reflow_profile;
	
	reflow_profile.Preheat = profile.pre_heat;
	reflow_profile.CoolRate = profile.cool_rate;
	reflow_profile.PeakTemp = profile.peak_temp;
	reflow_profile.SoakLen = profile.soak_length;
	reflow_profile.SoakTemp1 = profile.soak_temp1;
	reflow_profile.SoakTemp2 = profile.soak_temp2;
	reflow_profile.StartRate = profile.start_rate;
	reflow_profile.TimeToPeak = profile.time_to_peak;

	return reflow_profile;
}

Profile MapToProfile(ReflowProfile reflow_profile) {
	
	Profile profile;

	profile.pre_heat = reflow_profile.Preheat;
	profile.cool_rate = reflow_profile.CoolRate;
	profile.peak_temp = reflow_profile.PeakTemp;
	profile.soak_length = reflow_profile.SoakLen;
	profile.soak_temp1 = reflow_profile.SoakTemp1;
	profile.soak_temp2 = reflow_profile.SoakTemp2;
	profile.start_rate = reflow_profile.StartRate;
	profile.time_to_peak = reflow_profile.TimeToPeak;

	return profile;
}

void Execute(Request request, Response *response, ReflowContext *context) {
	
	response->Result = Response_ResultType_SUCCESS;

	switch (request.Command) {
		case Request_RequestType_STARTPROFILE: {
			
			uint16_t start_temp;
			
			start_temp = Get_TC_Temp();

			if(start_temp > Get_Profile().pre_heat)	{
				SetErrorResponse(response, Response_ErrorCodeType_OVENABOVETEMP);
			}
			else if(Get_Profile_State() == STOP) {
				response->Type = Response_ResponseType_STARTPROFILE;
				
				Reset_Pid();

				Set_Profile_Start();

				context->reflow_timer = 0;
				context->profile_time = 0;
				context->tc_temp = start_temp;
				context->target_temp = Get_Profile_Target_Temp(context->tc_temp, &context->reflow_timer);
				context->reflow_timer++;
				context->profile_time++;
				
				Setup_Timer_Counter1_PWM(&Handle_Timer_Overflow, TOP_MAX);
				
				Start_Timer_Counter1(TOP_MAX);
			}
			else
				SetErrorResponse(response, Response_ErrorCodeType_PROFILERUNNING);
		}
		break;
		case Request_RequestType_STOPPROFILE:
			if(Get_Profile_State() != STOP) {
				response->Type = Response_ResponseType_STOPPROFILE;
				Stop_Cycle();
			}
		break;
		case Request_RequestType_GETPROFILESTAGE:
			response->Type = Response_ResponseType_GETPROFILESTAGE;
			response->which_payload = Response_Stage_tag;
			response->payload.Stage = MapProfileStage(Get_Profile_State(), *context);
		break;
		case Request_RequestType_GETPROFILE:
			response->Type = Response_ResponseType_GETPROFILE;
			response->which_payload = Response_Profile_tag;
			response->payload.Profile = MapFromProfile(Get_Profile());
		break;
		case Request_RequestType_SETPROFILE:
			response->Type = Response_ResponseType_SETPROFILE;
			Save_Profile(MapToProfile(request.Profile));
		break;
		case Request_RequestType_TCSTATE:
			response->Type = Response_ResponseType_TCSTATE;
		
			if(Get_Profile_State() == STOP) {
				response->payload.TcState = Get_TC_State();
				response->which_payload = Response_TcState_tag;
			}
		else
			SetErrorResponse(response, Response_ErrorCodeType_PROFILERUNNING);
		
		break;
		case Request_RequestType_RELAYON:
		case Request_RequestType_RELAYOFF:
			response->Type = request.Command == Request_RequestType_RELAYON ? Response_ResponseType_RELAYON : Response_ResponseType_RELAYOFF;
		
			if(Get_Profile_State() == STOP)
				Set_Relay_State(request.Command == Request_RequestType_RELAYON);
		
		break;
		case Request_RequestType_PING:
			response->Type = Response_ResponseType_PING;
		break;
		case Request_RequestType_GETPID:
			response->Type = Response_ResponseType_GETPID;
			response->which_payload = Response_PidGains_tag;
						
			PidGains pid_gains = Get_Pid();

			response->payload.PidGains.kp = pid_gains.kp;
			response->payload.PidGains.ki = pid_gains.ki;
			response->payload.PidGains.kd = pid_gains.kd;
		break;
		case Request_RequestType_SETPID:
			response->Type = Response_ResponseType_SETPID;

			Set_Pid((PidGains){request.PidGains.kp, request.PidGains.ki, request.PidGains.kd});
		break;
		default:
			SetErrorResponse(response, Response_ErrorCodeType_UNKNOWNCOMMAND);
		break;
	}
}

void CDCTask() {
	
	if (USB_DeviceState != DEVICE_STATE_Configured)
		return;

	Endpoint_SelectEndpoint(CDC_RX_EPADDR);

	if (Endpoint_IsOUTReceived()) {
		memset(buffer, 0x00, sizeof(buffer));

		Endpoint_Read_Stream_LE(buffer, CDC_TXRX_EPSIZE, NULL);
		Endpoint_ClearOUT();

		response = Response_init_default;
		request = Request_init_default;

		pb_istream_t streamin = pb_istream_from_buffer(buffer, CDC_TXRX_EPSIZE);
		
		if (!pb_decode(&streamin, Request_fields, &request)) {
			response.Result = Response_ResultType_FAIL;
			response.payload.ErrorCode = Response_ErrorCodeType_UNKNOWNCOMMAND;
			response.which_payload = Response_ErrorCode_tag;
		}
		else {
			Execute(request, &response, &ctx);
		}
		
		pb_ostream_t streamout = pb_ostream_from_buffer(buffer, sizeof(buffer));
		
		pb_encode(&streamout, Response_fields, &response);
		
		Endpoint_SelectEndpoint(CDC_TX_EPADDR);
		Endpoint_Write_Stream_LE(buffer, streamout.bytes_written, NULL);
		bool IsFull = (Endpoint_BytesInEndpoint() == CDC_TXRX_EPSIZE);
		Endpoint_ClearIN();

		if (IsFull) {
			Endpoint_WaitUntilReady();
			Endpoint_ClearIN();
		}
	}
}

int main(void)
{
	SetupHardware();

	LEDs_SetAllLEDs(LEDMASK_USB_NOTREADY);
	
	GlobalInterruptEnable();
	
	Init_Profile();
	
	Init_Pid();

	while(1) {
		
		USB_USBTask();
		CDCTask();

		if(recalc_pid) {
			if (ctx.update++ == 5) {
				// Toggle the LED while running profile
				PORTD  ^= (1 << PORTD6);
				ctx.update = 0;
				ctx.tc_temp = Get_TC_Temp();
				ctx.target_temp = Get_Profile_Target_Temp(ctx.tc_temp, &ctx.reflow_timer);
				ctx.reflow_timer++;
				ctx.profile_time++;
			}
			
			OCR1A = Update_Pid(ctx.target_temp, ctx.tc_temp, TOP_MAX);

			recalc_pid = 0;

			if(Get_Profile_State() == STOP)	{	
				Stop_Cycle();
			}
		}
	}
}

/** Configures the board hardware and chip peripherals. */
void SetupHardware() {
	
	#if (ARCH == ARCH_AVR8)
	/* Disable watchdog if enabled by bootloader/fuses */
	MCUSR &= ~(1 << WDRF);
	wdt_disable();

	/* Disable clock division */
	clock_prescale_set(clock_div_1);
	#endif

	/* Hardware Initialization */
	LEDs_Init();
	USB_Init();

	TC_Setup();
	Init_Relay();
}

/** Event handler for the USB_Connect event. This indicates that the device is enumerating via the status LEDs. */
void EVENT_USB_Device_Connect(void) {
	/* Indicate USB enumerating */
	LEDs_SetAllLEDs(LEDMASK_USB_ENUMERATING);
}

/** Event handler for the USB_Disconnect event. This indicates that the device is no longer connected to a host via
*  the status LEDs.
*/
void EVENT_USB_Device_Disconnect(void) {
	/* Indicate USB not ready */
	LEDs_SetAllLEDs(LEDMASK_USB_NOTREADY);
}

/** Event handler for the USB_ConfigurationChanged event. This is fired when the host set the current configuration
*  of the USB device after enumeration - the device endpoints are configured and the CDC management task started.
*/
void EVENT_USB_Device_ConfigurationChanged(void) {
	bool ConfigSuccess = true;

	/* Setup CDC Data Endpoints */
	ConfigSuccess &= Endpoint_ConfigureEndpoint(CDC_NOTIFICATION_EPADDR, EP_TYPE_INTERRUPT, CDC_NOTIFICATION_EPSIZE, 1);
	ConfigSuccess &= Endpoint_ConfigureEndpoint(CDC_TX_EPADDR, EP_TYPE_BULK, CDC_TXRX_EPSIZE, 1);
	ConfigSuccess &= Endpoint_ConfigureEndpoint(CDC_RX_EPADDR, EP_TYPE_BULK,  CDC_TXRX_EPSIZE, 1);

	/* Reset line encoding baud rate so that the host knows to send new values */
	LineEncoding.BaudRateBPS = 0;

	/* Indicate endpoint configuration success or failure */
	LEDs_SetAllLEDs(ConfigSuccess ? LEDMASK_USB_READY : LEDMASK_USB_ERROR);
}

/** Event handler for the USB_ControlRequest event. This is used to catch and process control requests sent to
*  the device from the USB host before passing along unhandled control requests to the library for processing
*  internally.
*/
void EVENT_USB_Device_ControlRequest(void)
{
	/* Process CDC specific control requests */
	switch (USB_ControlRequest.bRequest)
	{
		case CDC_REQ_GetLineEncoding:
		if (USB_ControlRequest.bmRequestType == (REQDIR_DEVICETOHOST | REQTYPE_CLASS | REQREC_INTERFACE))
		{
			Endpoint_ClearSETUP();

			/* Write the line coding data to the control endpoint */
			Endpoint_Write_Control_Stream_LE(&LineEncoding, sizeof(CDC_LineEncoding_t));
			Endpoint_ClearOUT();
		}

		break;
		case CDC_REQ_SetLineEncoding:
		if (USB_ControlRequest.bmRequestType == (REQDIR_HOSTTODEVICE | REQTYPE_CLASS | REQREC_INTERFACE))
		{
			Endpoint_ClearSETUP();

			/* Read the line coding data in from the host into the global struct */
			Endpoint_Read_Control_Stream_LE(&LineEncoding, sizeof(CDC_LineEncoding_t));
			Endpoint_ClearIN();
		}

		break;
		case CDC_REQ_SetControlLineState:
		if (USB_ControlRequest.bmRequestType == (REQDIR_HOSTTODEVICE | REQTYPE_CLASS | REQREC_INTERFACE))
		{
			Endpoint_ClearSETUP();
			Endpoint_ClearStatusStage();

			/* NOTE: Here you can read in the line state mask from the host, to get the current state of the output handshake
			lines. The mask is read in from the wValue parameter in USB_ControlRequest, and can be masked against the
			CONTROL_LINE_OUT_* masks to determine the RTS and DTR line states using the following code:
			*/
		}

		break;
	}
}