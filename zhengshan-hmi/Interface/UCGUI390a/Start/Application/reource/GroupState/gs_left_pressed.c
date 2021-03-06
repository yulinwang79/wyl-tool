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
* Source file: gs_left_pressed
* Dimensions:  52 * 25
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

static GUI_CONST_STORAGE GUI_COLOR Colorsgs_left_pressed[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE Palgs_left_pressed = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &Colorsgs_left_pressed[0]
};

static GUI_CONST_STORAGE unsigned char acgs_left_pressed[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX____,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, _______X, XXXXXXXX, XXXXXXXX, XXXX____,
  XXXXXXXX, XXXXXXXX, XXXXXXX_, ________, __XXXXXX, XXXXXXXX, XXXX____,
  XX______, ________, ________, ________, ___XXXXX, XXXXXXXX, XXXX____,
  X_______, ________, ________, ________, ____XXXX, XX______, ___X____,
  ________, ________, ________, ________, ________, ________, ___X____,
  X_______, ________, ________, ________, ________, ________, ___X____,
  XX______, ________, ________, ________, ________, ________, ___X____,
  XXXX____, ________, ________, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXX___, ________, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXX___, ________, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXX___, ________, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXX__, ________, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, X_______, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, ________, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, ________, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, ________, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, XXX_____, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, XXXX____, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, XXX_____, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, XXX_____, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, XXX_____, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, XXXXX___, ________, ________, ________, ___X____,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XX______, _XXXXXXX, XX______, ___X____,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX____
};

GUI_CONST_STORAGE GUI_BITMAP bmgs_left_pressed = {
  52, /* XSize */
  25, /* YSize */
  7, /* BytesPerLine */
  1, /* BitsPerPixel */
  acgs_left_pressed,  /* Pointer to picture data (indices) */
  &Palgs_left_pressed  /* Pointer to palette */
};

/* *** End of file *** */
