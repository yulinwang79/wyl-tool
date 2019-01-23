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
* Source file: vv_stop_unpressed
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


static GUI_CONST_STORAGE GUI_LOGPALETTE Palvv_stop_unpressed = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &ColorsPalette[0]
};

static GUI_CONST_STORAGE unsigned char acvv_stop_unpressed[] = {
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
  ________, XXXXX__X, XXXXX__X, XXXXX__X, XXXXX___, ________,
  _______X, X____X__, _XX___XX, ____XX_X, X___XX__, ________,
  _______X, X____X__, _XX___XX, ____XX_X, X___XX__, ________,
  _______X, X_______, _XX___XX, ____XX_X, X___XX__, ________,
  ________, XXXXX___, _XX___XX, ____XX_X, X___XX__, ________,
  ________, ____XX__, _XX___XX, ____XX_X, XXXXX___, ________,
  _______X, ____XX__, _XX___XX, ____XX_X, X_______, ________,
  _______X, ____XX__, _XX___XX, ____XX_X, X_______, ________,
  ________, XXXXX___, _XX____X, XXXXX__X, X_______, ________,
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

GUI_CONST_STORAGE GUI_BITMAP bmvv_stop_unpressed = {
  43, /* XSize */
  32, /* YSize */
  6, /* BytesPerLine */
  1, /* BitsPerPixel */
  acvv_stop_unpressed,  /* Pointer to picture data (indices) */
  &Palvv_stop_unpressed  /* Pointer to palette */
};

/* *** End of file *** */
