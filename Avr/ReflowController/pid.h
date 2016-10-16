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

#ifndef PID_H_
#define PID_H_

#include <inttypes.h>

typedef struct _PidGains {
	int8_t kp;
	int8_t ki;
	int8_t kd;
} PidGains;

void Init_Pid();

void Reset_Pid();

void Set_Pid(PidGains new_pid_gains);

PidGains Get_Pid();

/**
 * \brief Calculate the correction factor
 * 
 * \param target_temp The target temperature
 * \param current_temp The current temperature
 * \param max_out
 * 
 * \return uint16_t
 */
uint16_t Update_Pid(int16_t target_temp, int16_t current_temp, uint16_t max_out);

#endif /* PID_H_ */