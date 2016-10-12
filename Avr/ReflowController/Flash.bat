dfu-programmer.exe atmega32u2 erase
dfu-programmer.exe atmega32u2 flash .\%1\ReflowController.hex
dfu-programmer.exe atmega32u2 flash --force --eeprom .\%1\ReflowController.eep