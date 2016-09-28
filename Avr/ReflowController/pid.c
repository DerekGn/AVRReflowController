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
#include "shared.h"

#define MAXTEMP 340

#define PID_P 360;
#define PID_I 80;
#define PID_D 0;

static int16_t last_error = 0;
static int16_t integral = 0;

static uint16_t Approximate_PWM(uint16_t target, uint16_t max_top) {
	int32_t t;
	
	t = ((max_top * target) / (MAXTEMP*4));

	return (uint16_t) CLAMP(t, 0, max_top);
}

uint16_t pid(uint16_t target_temp, uint16_t current_temp, uint16_t max_top) {
	
	int32_t error = (int32_t)target_temp - (int32_t)current_temp;

	if (target_temp == 0) {
		integral = 0;
		last_error = error;
		return 0;
	} else {

		int32_t p_term = error * PID_P;
		int32_t i_term = integral * PID_I;
		int32_t d_term = (last_error - error) * PID_D;

		int16_t new_integral = integral + error;
		/* Clamp integral to a reasonable value */
		new_integral = CLAMP(new_integral, -4 * 100, 4 * 100);

		last_error = error;

		int32_t result = Approximate_PWM(target_temp, max_top) + p_term + i_term + d_term;

		/* Avoid integral buildup */
		if ((result >= max_top && new_integral < integral) || (result < 0 && new_integral > integral) || (result <= max_top && result >= 0)) {
			integral = new_integral;
		}

		/* Clamp the output value */
		return (uint16_t)(CLAMP(result, 0, max_top));
	}
}
