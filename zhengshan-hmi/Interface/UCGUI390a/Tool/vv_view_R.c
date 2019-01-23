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
* Source file: vv_view_R
* Dimensions:  35 * 26
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

static GUI_CONST_STORAGE GUI_COLOR Colorsvv_view_R[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE Palvv_view_R = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &Colorsvv_view_R[0]
};

static GUI_CONST_STORAGE unsigned char acvv_view_R[] = {
  XXXXXXXX, ________, ________, ___XXXXX, XXX_____,
  XXXXX___, XXXXXXXX, XXXXXXXX, XXX___XX, XXX_____,
  XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XXX_____,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, XXX_____,
  XX_XXXXX, XXX_____, _____XXX, XXXXXXXX, _XX_____,
  X_XXXXXX, XXX_____, ______XX, XXXXXXXX, X_X_____,
  X_XXXXXX, XXX_____, ______XX, XXXXXXXX, X_X_____,
  X_XXXXXX, XXX___XX, XXXX___X, XXXXXXXX, X_X_____,
  _XXXXXXX, XXX___XX, XXXX___X, XXXXXXXX, XX______,
  _XXXXXXX, XXX___XX, XXXX___X, XXXXXXXX, XX______,
  _XXXXXXX, XXX_____, ______XX, XXXXXXXX, XX______,
  _XXXXXXX, XXX_____, _____XXX, XXXXXXXX, XX______,
  _XXXXXXX, XXX_____, ___XXXXX, XXXXXXXX, XX______,
  _XXXXXXX, XXX___XX, X___XXXX, XXXXXXXX, XX______,
  _XXXXXXX, XXX___XX, XX___XXX, XXXXXXXX, XX______,
  _XXXXXXX, XXX___XX, XX____XX, XXXXXXXX, XX______,
  _XXXXXXX, XXX___XX, XXX___XX, XXXXXXXX, XX______,
  X_XXXXXX, XXX___XX, XXXX___X, XXXXXXXX, X_X_____,
  X_XXXXXX, XXX___XX, XXXX____, XXXXXXXX, X_X_____,
  X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_X_____,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, _XX_____,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, XXX_____,
  XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XXX_____,
  XXXXX___, XXXXXXXX, XXXXXXXX, XXX___XX, XXX_____,
  XXXXXXXX, ________, ________, ___XXXXX, XXX_____,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_____
};

GUI_CONST_STORAGE GUI_BITMAP bmvv_view_R = {
  35, /* XSize */
  26, /* YSize */
  5, /* BytesPerLine */
  1, /* BitsPerPixel */
  acvv_view_R,  /* Pointer to picture data (indices) */
  &Palvv_view_R  /* Pointer to palette */
};

/* *** End of file *** */
