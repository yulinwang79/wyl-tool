/*********************************************************************
*                SEGGER MICROCONTROLLER SYSTEME GmbH                 *
*        Solutions for real time microcontroller applications        *
*                           www.segger.com                           *
**********************************************************************
*
* C-file generated by
*
*        �C/GUI-BitmapConvert V3.90.
*        Compiled Aug 19 2004, 09:07:56
*          (c) 2002  Micrium, Inc.
  www.micrium.com

  (c) 1998-2002  Segger
  Microcontroller Systeme GmbH
  www.segger.com
*
**********************************************************************
*
* Source file: VV_BUTTON_BG
* Dimensions:  45 * 34
* NumColors:   2
*
**********************************************************************
*/

#include "stdlib.h"

#include "GUI.h"

#ifndef GUI_CONST_STORAGE
  #define GUI_CONST_STORAGE const
#endif

/*   Palette
The following are the entries of the palette table.
Every entry is a 32-bit value (of which 24 bits are actually used)
the lower   8 bits represent the Red component,
the middle  8 bits represent the Green component,
the highest 8 bits (of the 24 bits used) represent the Blue component
as follows:   0xBBGGRR
*/

static GUI_CONST_STORAGE GUI_COLOR ColorsVV_BUTTON_BG[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE PalVV_BUTTON_BG = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &ColorsVV_BUTTON_BG[0]
};

static GUI_CONST_STORAGE unsigned char acVV_BUTTON_BG[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, X_______, ________, ________, ____XXXX, XXXXX___,
  XXXXXX__, ________, ________, ________, _______X, XXXXX___,
  XXXXX___, ________, ________, ________, ________, XXXXX___,
  XXXX____, ________, ________, ________, ________, _XXXX___,
  XXX_____, ________, ________, ________, ________, __XXX___,
  XX______, ________, ________, ________, ________, ___XX___,
  XX______, ________, ________, ________, ________, ___XX___,
  XX______, ________, ________, ________, ________, ___XX___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  X_______, ________, ________, ________, ________, ____X___,
  XX______, ________, ________, ________, ________, ___XX___,
  XX______, ________, ________, ________, ________, ___XX___,
  XX______, ________, ________, ________, ________, ___XX___,
  XXX_____, ________, ________, ________, ________, __XXX___,
  XXXX____, ________, ________, ________, ________, _XXXX___,
  XXXXX___, ________, ________, ________, ________, XXXXX___,
  XXXXXX__, ________, ________, ________, _______X, XXXXX___,
  XXXXXXXX, X_______, ________, ________, ____XXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___
};

GUI_CONST_STORAGE GUI_BITMAP bmVV_BUTTON_BG = {
  45, /* XSize */
  34, /* YSize */
  6, /* BytesPerLine */
  1, /* BitsPerPixel */
  acVV_BUTTON_BG,  /* Pointer to picture data (indices) */
  &PalVV_BUTTON_BG  /* Pointer to palette */
};

/* *** End of file *** */