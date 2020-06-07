// OnBoardPilotSystem.h
/***********************************
*	HOPLIK SYSTEMS 24.02.2015  *
*	Welcome http://hoplik.ru/  *
***********************************/
/*
	Система подключения бортового автопилота. Функциязамены данных шести каналов с передатчика на данные из
	ком-порта.
	*/

#ifndef _ONBOARDPILOTSYSTEM_h
#define _ONBOARDPILOTSYSTEM_h

#if defined(ARDUINO) && ARDUINO >= 100
#include "Arduino.h"
#else
#include "WProgram.h"
#endif

#include "RangeFinder.h"

int PilotSystemCommand[10] = { 1500, 1500, 1500, 1000, 2000, 2000, 0, 0, 0, 0 };	// Массив данных для подмены

String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete

#endif

