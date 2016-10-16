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

#include "pid.h"

#include <avr/eeprom.h>
#include <string.h>

#define DIV 8
#define DELAY 40

typedef struct _PidData {
	int16_t pid_prev[DELAY]; // previous temperatures (0.25C units)
	uint8_t pid_prev_index;
	int16_t last_error;
	int16_t integral;
	PidGains gains;
} PidData;

static PidGains EEMEM eeprom_pid_gains = {
	.kp = 13,
	.ki = 3,
	.kd = 13
};

static PidData pidData;

int16_t Pid_Prev_Update(int16_t prev)
{
	int16_t popped = pidData.pid_prev[pidData.pid_prev_index];
	pidData.pid_prev[pidData.pid_prev_index] = prev;

	pidData.pid_prev_index++;
	
	if(pidData.pid_prev_index >= DELAY)
		pidData.pid_prev_index = 0;

	return popped;
}

void Init_Pid() {
	eeprom_read_block(&pidData.gains, &eeprom_pid_gains, sizeof(PidGains));
}

void Reset_Pid() {
	
	memset(&pidData.pid_prev, 0, DELAY);

	pidData.pid_prev_index = 0;
	pidData.integral = 0;
	pidData.last_error = 0;
}

void Set_Pid(PidGains new_pid_gains)
{
	memcpy(&pidData.gains, &new_pid_gains, sizeof(PidGains));
	eeprom_update_block(&pidData.gains, &eeprom_pid_gains, sizeof(PidGains));
}

PidGains Get_Pid()
{
	return pidData.gains;
}

uint16_t Update_Pid(int16_t target_temp, int16_t current_temp, uint16_t max_out) {
	int16_t error, derivative;
	int32_t pwm;

	// calculate terms
	error       = target_temp - current_temp; // error term must be positive when we're ramping up
	derivative  = Pid_Prev_Update(current_temp) - current_temp; // derivative term must be negative when we're ramping up

	// sum weighted terms
	pwm = ((int32_t)error) << pidData.gains.kp;
	pwm += ((int32_t)pidData.integral) << pidData.gains.ki;
	pwm += ((int32_t)derivative) << pidData.gains.kd;

	// post-divide
	pwm >>= DIV;

	// only update integral if output is not saturated (or if change would reduce saturation)
	if( (pwm >= 0 && pwm <= max_out) || (pwm > 0 && error < 0) || (pwm < 0 && error > 0) )
		pidData.integral += error;

	// limit command
	if(pwm < 0)
		pwm = 0;
	else if(pwm > max_out)
		pwm = max_out;

	return (uint16_t) pwm;
}
