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
* Source file: vv_left
* Dimensions:  53 * 47
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

static GUI_CONST_STORAGE GUI_COLOR Colorsvv_left[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE Palvv_left = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &Colorsvv_left[0]
};

static GUI_CONST_STORAGE unsigned char acvv_left[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, X_______, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, _XXXXXXX, ___XXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, _XXXXXXX, XXX_XXXX, XXXXXXXX, XXXXX___,
  XXX_____, ________, ________, XXXXXXXX, XXX_XXXX, XXXXXXXX, XXXXX___,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XXX, XXX_____, ____X___,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXX_XXX, XXX_X___,
  X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___, ______XX, XXX_X___,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXX__XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXX___, ________, __XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXX_, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXX_X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXX_X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXX_X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXX_X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXX_, ____XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, X___XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XXXXXX__, ___XXXXX, XX______, ______XX, XXX_X___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_____, __XXXXXX, XXX_____, ____X___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___
};

GUI_CONST_STORAGE GUI_BITMAP bmvv_left = {
  53, /* XSize */
  47, /* YSize */
  7, /* BytesPerLine */
  1, /* BitsPerPixel */
  acvv_left,  /* Pointer to picture data (indices) */
  &Palvv_left  /* Pointer to palette */
};

/* *** End of file *** */