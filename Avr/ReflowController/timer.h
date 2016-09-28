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

#ifndef TIMER_H_
#define TIMER_H_

#include <inttypes.h>

/**
* \brief The call back invoked when Timer1 output compare B match interrupt is triggered
*
*/
typedef void (timer_counter_overflow)();

/**
* \brief
*
* \param timer_counter_overflow_func The callback function to invoke when
* \param top The compare value for the output compare 1A
*
* \return void
*/
void Setup_Timer_Counter1_PWM(timer_counter_overflow *timer_counter_overflow_func, uint16_t top);

/**
* \brief Restores Timer1 back to default state. Stops Timer1 and resets count registers
*
*
* \return void
*/
void Restore_Timer_Counter1();

/**
* \brief Starts Timer 1. Resets the Timer1 count registers. Enables OCIE1B interrupt. Enables Timer1 clock clkio/256
*
* \param top The max value for ICR1 and OCR1A
*
* \return void
*/
void Start_Timer_Counter1(uint16_t top);

/**
* \brief Stops Timer1. Disable OCIE1B interrupt and disables Timer1 clock
*
*
* \return void
*/
void Stop_Timer_Counter1();

#endif /* TIMER_H_ */