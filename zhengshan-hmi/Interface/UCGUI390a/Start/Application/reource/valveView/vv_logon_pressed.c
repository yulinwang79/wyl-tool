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
* Source file: vv_logon_pressed
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

static GUI_CONST_STORAGE GUI_COLOR Colorsvv_logon_pressed[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE Palvv_logon_pressed = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &Colorsvv_logon_pressed[0]
};

static GUI_CONST_STORAGE unsigned char acvv_logon_pressed[] = {
  XXXXXXXX, ________, ________, ________, ________, ________, ________, __XXXXXX, XX______,
  XXXXX___, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XX___XXX, XX______,
  XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XX______,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XX______,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, XX______,
  X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, _X______,
  X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, _X______,
  X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, _X______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXX__, XXXXXX__, ___XXXX_, ___XXXX_, ____XXX_, _XX__XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXX__, XXXXX__X, XX__XX__, XX__XX__, XXX__XX_, _XX__XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXX__, XXXXX__X, XX__XX_X, XXXXXX__, XXX__XX_, __X__XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXX__, XXXX__XX, XXX____X, XXXXX__X, XXXX__X_, __X__XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXX__, XXXX__XX, XXX____X, X___X__X, XXXX__X_, _____XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXX__, XXXX__XX, XXX____X, XXX_X__X, XXXX__X_, _X___XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXX__, XXXXX__X, XX__XX_X, XXX_XX__, XXX__XX_, _X___XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXX__, XXXXX__X, XX__XX__, XX__XX__, XXX__XX_, _XX__XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXX__, ____XX__, ___XXXX_, ____XXX_, ____XXX_, _XX__XXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______,
  X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, _X______,
  X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, _X______,
  X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, _X______,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, XX______,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XX______,
  XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XX______,
  XXXXX___, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XX___XXX, XX______,
  XXXXXXXX, ________, ________, ________, ________, ________, ________, __XXXXXX, XX______
};

GUI_CONST_STORAGE GUI_BITMAP bmvv_logon_pressed = {
  66, /* XSize */
  36, /* YSize */
  9, /* BytesPerLine */
  1, /* BitsPerPixel */
  acvv_logon_pressed,  /* Pointer to picture data (indices) */
  &Palvv_logon_pressed  /* Pointer to palette */
};

/* *** End of file *** */
