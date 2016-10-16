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

#include "profile.h"

#include <avr/eeprom.h>

static ProfileState reflow_state;
static uint16_t profile_start_temp;
static Profile profile;

static Profile EEMEM eeprom_profile = {
	.pre_heat = 40,
	.start_rate = 4,
	.soak_temp1 = 600,
	.soak_temp2 = 720,
	.soak_length = 100,
	.peak_temp = 800,
	.time_to_peak = 80,
	.cool_rate = 4
};

uint16_t Get_Profile_Target_Temp(uint16_t temp, uint16_t *timer) {
	
	uint16_t target = 0;
	switch (reflow_state)
	{
		case STOP:
			target = 0;
			reflow_state = STOP;
		break;
		case PREHEAT:
			target = profile.pre_heat;
			
			if (temp >= profile.pre_heat) {
				*timer = 0;
				reflow_state = START;
			}
		break;
		case START:
			target = profile.pre_heat + (profile.start_rate * *timer);

			if (target > profile.soak_temp1) {
				target = profile.soak_temp1;
			}

			if (temp >= profile.soak_temp1) {
				*timer = 0;
				reflow_state = SOAK;
			}
		break;
		case SOAK:
			if ((*timer) < profile.soak_length) {
				target = profile.soak_temp1 + ((*timer)*(profile.soak_temp2 - profile.soak_temp1)) / profile.soak_length;
			}
			else {
				target = profile.soak_temp2;
				if (temp >= profile.soak_temp2) {
					*timer = 0;
					reflow_state = PEAK;
				}
			}
		break;
		case PEAK:
			if ((*timer) < profile.time_to_peak) {
				target = profile.soak_temp2 +
				((*timer)*(profile.peak_temp - profile.soak_temp2)) / profile.time_to_peak;
			}
			else {
				target = profile.peak_temp;
				if (temp >= target) {
					*timer = 0;
				
					reflow_state = COOL;
				}
			}
		break;
		case COOL:
			target = profile.peak_temp - (profile.cool_rate * *timer);

			if (target <= profile_start_temp)
				reflow_state = STOP;

		break;
		default:
			reflow_state = STOP;
		break;
	}
	
	return target;
}

void Save_Profile(Profile new_profile) {
	memcpy(&profile, &new_profile, sizeof(Profile));
	eeprom_update_block(&profile, &eeprom_profile ,sizeof(profile));
}

Profile Get_Profile() {
	return profile;
}

void Init_Profile() {
	eeprom_read_block(&profile, &eeprom_profile, sizeof(profile));
	reflow_state = STOP;
}

ProfileState Get_Profile_State()
{
	return reflow_state;
}

void Set_Profile_Start()
{
	reflow_state = PREHEAT;
}

void Set_Profile_Stop()
{
	reflow_state = STOP;
}
