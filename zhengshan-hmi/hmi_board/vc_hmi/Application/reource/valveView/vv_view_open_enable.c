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
* Source file: vv_view_open_enable
* Dimensions:  43 * 32
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


static GUI_CONST_STORAGE GUI_LOGPALETTE Palvv_view_open_enable = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &ColorsPalette[0]
};

static GUI_CONST_STORAGE unsigned char acvv_view_open_enable[] = {
  XXXXXXXX, ________, ________, ________, ___XXXXX, XXX_____,
  XXXXX___, ________, ________, ________, ______XX, XXX_____,
  XXXX____, ________, ________, ________, _______X, XXX_____,
  XXX_____, ________, ________, ________, ________, XXX_____,
  XX______, ________, ________, ________, ________, _XX_____,
  X_______, ________, ________, ________, ________, __X_____,
  X_______, ________, ________, ________, ________, __X_____,
  X_______, ________, ________, ________, ________, __X_____,
  ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________,
  _______X, XXXXX__X, XXXXX__X, XXXXX_XX, ____X___, ________,
  ______XX, ____XX_X, X___XX_X, X_____XX, X___X___, ________,
  ______XX, ____XX_X, X___XX_X, X_____XX, X___X___, ________,
  ______XX, ____XX_X, X___XX_X, X_____X_, XX__X___, ________,
  ______XX, ____XX_X, X___XX_X, XXXXX_X_, _XX_X___, ________,
  ______XX, ____XX_X, XXXXX__X, X_____X_, _XX_X___, ________,
  ______XX, ____XX_X, X______X, X_____X_, __XXX___, ________,
  ______XX, ____XX_X, X______X, X_____X_, __XXX___, ________,
  _______X, XXXXX__X, X______X, XXXXX_X_, ___XX___, ________,
  ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________,
  X_______, ________, ________, ________, ________, __X_____,
  X_______, ________, ________, ________, ________, __X_____,
  X_______, ________, ________, ________, ________, __X_____,
  XX______, ________, ________, ________, ________, _XX_____,
  XXX_____, ________, ________, ________, ________, XXX_____,
  XXXX____, ________, ________, ________, _______X, XXX_____,
  XXXXX___, ________, ________, ________, ______XX, XXX_____,
  XXXXXXXX, ________, ________, ________, ___XXXXX, XXX_____
};

GUI_CONST_STORAGE GUI_BITMAP bmvv_view_open_enable = {
  43, /* XSize */
  32, /* YSize */
  6, /* BytesPerLine */
  1, /* BitsPerPixel */
  acvv_view_open_enable,  /* Pointer to picture data (indices) */
  &Palvv_view_open_enable  /* Pointer to palette */
};

/* *** End of file *** */