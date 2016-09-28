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

#include "timer.h"

#include <avr/io.h>
#include <avr/interrupt.h>

static timer_counter_overflow *timer_counter_overflow_handler;

ISR(TIMER1_COMPB_vect) {

	if(timer_counter_overflow_handler)
	timer_counter_overflow_handler();
}

static void Reset_Timer_Count(uint16_t top, uint16_t bottom) {
	TCNT1 = 0x00;
	ICR1 = top;
	OCR1B = bottom;
	OCR1A = top;
}

void Setup_Timer_Counter1_PWM(timer_counter_overflow *timer_counter_overflow_func, uint16_t top) {
	
	TCCR1A |= (1 << COM1A1) | (1 << WGM11) | (0 << WGM10);
	TCCR1B |= (1 << WGM13) | (1 << WGM12);
	
	Reset_Timer_Count(top, 1);

	timer_counter_overflow_handler = timer_counter_overflow_func;
}

void Restore_Timer_Counter1() {
	Stop_Timer_Counter1();

	TCCR1A = 0;
	TCCR1B = 0;

	Reset_Timer_Count(0,0);
}

void Start_Timer_Counter1(uint16_t top) {
	Reset_Timer_Count(top, 1);
	
	// Enable OCIE1B interrupt
	TIMSK1 = (1 << OCIE1B);

	// Enable timer clock clkio/256
	TCCR1B |= (1 << CS12) | (0 << CS11) | (0 << CS10);
}

void Stop_Timer_Counter1() {
	// Disable OCIE1B interrupt
	TIMSK1 = (1 << OCIE1B);
	// Disable timer1 clock
	TCCR1B &= ~((1 << CS12) | (1 << CS11) | (1 << CS10));
}
