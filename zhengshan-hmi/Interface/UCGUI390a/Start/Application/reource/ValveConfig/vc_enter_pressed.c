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
* Source file: vc_enter_pressed
* Dimensions:  66 * 36
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

static GUI_CONST_STORAGE GUI_COLOR Colorsvc_enter_pressed[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE Palvc_enter_pressed = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &Colorsvc_enter_pressed[0]
};

static GUI_CONST_STORAGE unsigned char acvc_enter_pressed[] = {
  XXXXXX__, ________, ________, ________, ________, ________, ________, ___XXXXX, XX______,
  XXXXX___, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X____XXX, XX______,
  XXXX__XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX__XX, XX______,
  XXX__XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX__X, XX______,
  XX__XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX__, XX______,
  X__XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, _X______,
  X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, _X______,
  __XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, ________,
  __XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, ________,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XX__XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XX__XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, X___XXXX, __X____X, X_____XX, X___XXXX, __X__XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, _____XXX, ________, X_____XX, _____XXX, _____XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXX_, _XXX__XX, ___XXX__, XX__XXX_, _XXX__XX, ___XXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXX_, ______XX, __XXXX__, XX__XXX_, ______XX, __XXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXX_, ______XX, __XXXX__, XX__XXX_, ______XX, __XXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXX_, _XXXXXXX, __XXXX__, XX__XXX_, _XXXXXXX, __XXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXX_, __XX__XX, __XXXX__, XX__XXX_, __XX__XX, __XXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, _____XXX, __XXXX__, XX____XX, _____XXX, __XXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, X___XXXX, __XXXX__, XXX___XX, X___XXXX, __XXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  __XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, ________,
  __XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, ________,
  X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, ________,
  X__XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, _X______,
  XX__XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX__, XX______,
  XXX__XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX__X, XX______,
  XXXX__XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX__XX, XX______,
  XXXXX___, _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X____XXX, XX______,
  XXXXXXX_, ________, ________, ________, ________, ________, ________, ___XXXXX, XX______
};

GUI_CONST_STORAGE GUI_BITMAP bmvc_enter_pressed = {
  66, /* XSize */
  36, /* YSize */
  9, /* BytesPerLine */
  1, /* BitsPerPixel */
  acvc_enter_pressed,  /* Pointer to picture data (indices) */
  &Palvc_enter_pressed  /* Pointer to palette */
};

/* *** End of file *** */
