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

#ifndef PROFILE_H_
#define PROFILE_H_

#include <inttypes.h>
#include <LUFA/Common/Common.h>

/* Type Defines: */
/** \brief Profile description
*
*	Type definition for the reflow profile. It defines the various stages of a reflow profile.
*/
typedef struct _Profile
{
	uint16_t pre_heat;		// The pre heat in °C
	uint16_t start_rate;	// The ramp to soak rate in °C per second
	uint16_t soak_temp1;	// The start preheat/soak temperature
	uint16_t soak_temp2;	// The end preheat/soak temperature
	uint16_t soak_length;	// The length of time in seconds for the preheat/soak
	uint16_t peak_temp;		// The peak temperature for the reflow temperature
	uint16_t time_to_peak;	// The amount of time to peak temperature
	uint16_t cool_rate;		// The cool down rate in °C per second
} Profile;

/* Type Defines: */
/** \brief The various stages of the reflow profile
*
*/
typedef enum _ProfileState
{
	STOP	= 0,		// The profile has entered stopped state
	START	= 1,		// The profile has started
	SOAK	= 2,		// The profile is in soak stage
	PEAK	= 3,		// The profile is in peak
	COOL	= 4,		// The profile is in cool down
	PREHEAT	= 5,		// The profile has entered preheat phase
} ProfileState;

/**
* \brief Calculate the new target temperature and update the profile stage if required
*
* \param temp The current temperature as read from the Thermocouple
* \param timer	The current time interval
*
* \return uint16_t
*/
uint16_t Get_Profile_Target_Temp(uint16_t temp, uint16_t *timer);

/**
* \brief Saves an updated reflow profile to eeprom and updates the current in memory profile
*
* \param new_profile The new profile to save
*
* \return void
*/
void Save_Profile(Profile new_profile);

/**
* \brief Get the current active profile
*
*
* \return Profile The current profile read
*/
Profile Get_Profile();

/**
* \brief Read the profile from eeprom memory in to ram.
*
*
* \return void
*/
void Init_Profile();

/**
* \brief Get the current profile stage
*
*
* \return ProfileState The current profile stage
*/
ProfileState Get_Profile_State();

void Set_Profile_Start(uint16_t start_temp);

void Set_Profile_Stop();

#endif