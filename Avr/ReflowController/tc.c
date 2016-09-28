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

#include "tc.h"
#include <avr/io.h>
#include <util/delay.h>

#define DDR_SPI DDRB
#define DD_MOSI PINB2
#define DD_SCK	PINB1
#define SS_PIN	PINB0

#define AVERAGE_BITS 2
#define AVERAGE (1<<AVERAGE_BITS)

static volatile int16_t tcstate;

int16_t temps[AVERAGE];

static void SetSS(uint8_t ss) {
	
	if(ss)
		PORTB &= ~(1 << SS_PIN);
	else
		PORTB |= (1 << SS_PIN);
}

void TC_Setup() {
	
	DDRB |= (1 << SS_PIN);

	DDR_SPI = (1 << DD_MOSI) | (1 << DD_SCK);
	
	SPCR = (1 << SPE) | (1 << MSTR);
	
	SetSS(1);
	_delay_us(100);
	SetSS(0);
}


uint16_t Get_TC_State() {

	uint16_t tcstate = 0;

	SetSS(1);

	// read MSbyte
	SPDR = 0xFF;
	while(!(SPSR & (1<<SPIF)))
	tcstate = SPDR;
	tcstate <<= 8;

	// read LSbyte
	SPDR = 0xFF;
	while(!(SPSR & (1<<SPIF)))
	tcstate |= SPDR;
	
	SetSS(0);

	return tcstate;
}

uint16_t Get_TC_Temp() {
	
	int16_t result;
	int16_t avg;

	result = Get_TC_State() >> 3;

	// average
	avg = result;
	for(uint8_t i=1; i<AVERAGE; ++i)
	{
		temps[i] = temps[i-1];
		avg += temps[i];
	}
	temps[0] = result;

	avg >>= AVERAGE_BITS;

	return avg;
}

