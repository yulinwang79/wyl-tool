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
* Source file: vv_logout_unpressed
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

static GUI_CONST_STORAGE GUI_COLOR Colorsvv_logout_unpressed[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE Palvv_logout_unpressed = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &Colorsvv_logout_unpressed[0]
};

static GUI_CONST_STORAGE unsigned char acvv_logout_unpressed[] = {
  XXXXXXXX, ________, ________, ________, ________, ________, ________, __XXXXXX, XX______,
  XXXXX___, ________, ________, ________, ________, ________, ________, _____XXX, XX______,
  XXXX____, ________, ________, ________, ________, ________, ________, ______XX, XX______,
  XXX_____, ________, ________, ________, ________, ________, ________, _______X, XX______,
  XX______, ________, ________, ________, ________, ________, ________, ________, XX______,
  X_______, ________, ________, ________, ________, ________, ________, ________, _X______,
  X_______, ________, ________, ________, ________, ________, ________, ________, _X______,
  X_______, ________, ________, ________, ________, ________, ________, ________, _X______,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  ________, __XX____, __XXXXXX, ___XXXXX, ___XXXXX, X__XX___, XX_XXXXX, X_______, ________,
  ________, __XX____, _XX____X, X_XX____, X_XX____, XX_XX___, XX___XX_, ________, ________,
  ________, __XX____, _XX____X, X_XX____, X_XX____, XX_XX___, XX___XX_, ________, ________,
  ________, __XX____, _XX____X, X_XX____, __XX____, XX_XX___, XX___XX_, ________, ________,
  ________, __XX____, _XX____X, X_XX__XX, X_XX____, XX_XX___, XX___XX_, ________, ________,
  ________, __XX____, _XX____X, X_XX___X, X_XX____, XX_XX___, XX___XX_, ________, ________,
  ________, __XX____, _XX____X, X_XX___X, X_XX____, XX_XX___, XX___XX_, ________, ________,
  ________, __XX____, _XX____X, X_XX___X, X_XX____, XX_XX___, XX___XX_, ________, ________,
  ________, __XXXXXX, __XXXXXX, ___XXXXX, X__XXXXX, X___XXXX, X____XX_, ________, ________,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  ________, ________, ________, ________, ________, ________, ________, ________, ________,
  X_______, ________, ________, ________, ________, ________, ________, ________, _X______,
  X_______, ________, ________, ________, ________, ________, ________, ________, _X______,
  X_______, ________, ________, ________, ________, ________, ________, ________, _X______,
  XX______, ________, ________, ________, ________, ________, ________, ________, XX______,
  XXX_____, ________, ________, ________, ________, ________, ________, _______X, XX______,
  XXXX____, ________, ________, ________, ________, ________, ________, ______XX, XX______,
  XXXXX___, ________, ________, ________, ________, ________, ________, _____XXX, XX______,
  XXXXXXXX, ________, ________, ________, ________, ________, ________, __XXXXXX, XX______
};

GUI_CONST_STORAGE GUI_BITMAP bmvv_logout_unpressed = {
  66, /* XSize */
  36, /* YSize */
  9, /* BytesPerLine */
  1, /* BitsPerPixel */
  acvv_logout_unpressed,  /* Pointer to picture data (indices) */
  &Palvv_logout_unpressed  /* Pointer to palette */
};

/* *** End of file *** */
