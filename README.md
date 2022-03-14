# DiverBoard
A windows tablet touch/barcode based diver tracking system for scuba diving liveaboard operators.

## Config File

DiverBoard will create the file C:\DiverBoard\Config\Bunks.txt.  This is a simple list of bunk names for your boat.  The defaults are the bunks used by the M/V Fling Freeport, TX. Once updated your custom list of bunk names will be used when creating all dive trips.

## Trip Files

DiverBoard wil create trip files named C:\DiverBoard\Config\Trip-{MMDDYYYY}.json.  You can stick a USB thumb drive into the computer and click the USB icon from the Configuration screen to archive your trip files to the thumb drive for safe keeping.

## Main Menu

There are icons that act like the capslock on your keyboard.  They set the operations mode:

Divers Entering Water
Undo Divers Entering  Water
Divers Returning from Water
Undo Divers Returning from Water

## Barcode Reader Notes ##

Diverboard suppports a generic keyboard wedge style barcode scanner. These scanners can be hooked up via USB or USB dongle and simulate keyboard entry.  The following commands are supported and can be typed on the keyboard to simulate a barcode scanner if you do not yet have one.

<C0> Command Mode Divers Entering Water
<C1> Command Mode Undo Divers Entering Water
<C2> Command Mode Divers Returning from Water
<C3> Command Mode Undo Divers Returning from Water
  
<BUNKNAME>  BUNKNAMES matching the list of bunk names you definded.  This takes the current command mode and applies it to said bunk/diver.
 
