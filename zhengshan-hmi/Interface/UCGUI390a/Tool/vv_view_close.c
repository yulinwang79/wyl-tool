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
* Source file: vv_view_close
* Dimensions:  145 * 32
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

static GUI_CONST_STORAGE GUI_COLOR Colorsvv_view_close[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE Palvv_view_close = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &Colorsvv_view_close[0]
};

static GUI_CONST_STORAGE unsigned char acvv_view_close[] = {
  _XXXXXXX, XXXX____, ________, ________, _______X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, ________, ________, ________, ___XXXXX, XXXXXXXX, ________,
  _XXXXXXX, X___XXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, __XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___, ________, ________, ________, ______XX, XXXXXXXX, ________,
  _XXXXXXX, _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX____, ________, ________, ________, _______X, XXXXXXXX, ________,
  _XXXXXX_, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_____, ________, ________, ________, ________, XXXXXXXX, ________,
  _XXXXX_X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, XXXXXX__, ________, ________, _XXXXXXX, XXXXXXXX, XX______, ________, ________, ________, ________, _XXXXXXX, ________,
  _XXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XXX___XX, XXXXXXXX, XXXXXXXX, X___XXXX, XXXXXXXX, X_______, ________, ________, ________, ________, __XXXXXX, ________,
  _XXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, X_______, ________, ________, ________, ________, __XXXXXX, ________,
  _XXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, X_______, ________, ________, ________, ________, __XXXXXX, ________,
  _XXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XXXXXXXX, _XXXXXXX, X_______, ___XXXXX, XXXXXX_X, XXXXXXXX, ________, ________, ________, ________, ________, ___XXXXX, ________,
  _XXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XXXXXXX_, XXXXXXXX, X_______, ____XXXX, XXXXXXX_, XXXXXXXX, ________, ________, ________, ________, ________, ___XXXXX, ________,
  _XXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XXXXXXX_, XXXXXXXX, X_______, ____XXXX, XXXXXXX_, XXXXXXXX, ________, ________, ________, ________, ________, ___XXXXX, ________,
  _XXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XXXXXXX_, XXXXXXXX, X___XXXX, XX___XXX, XXXXXXX_, XXXXXXXX, ________, ________, ________, ________, ________, ___XXXXX, ________,
  _XXX_XXX, XXX_____, _XX_____, _XX_____, _X__XXXX, _XXXXX_X, XXXXXX_X, XXXXXXXX, X___XXXX, XX___XXX, XXXXXXXX, _XXXXXXX, ____XXXX, X__XX___, ___XXXXX, X___XXXX, X__XXXXX, X__XXXXX, ________,
  _XXX_XXX, XX__XXXX, __X__XXX, __X__XXX, XX___XXX, _XXXXX_X, XXXXXX_X, XXXXXXXX, X___XXXX, XX___XXX, XXXXXXXX, _XXXXXXX, ___XX___, _X_XX___, __XX____, XX_XX___, _X_XX___, ___XXXXX, ________,
  _XXX_XXX, XX__XXXX, __X__XXX, __X__XXX, XX___XXX, _XXXXX_X, XXXXXX_X, XXXXXXXX, X_______, ____XXXX, XXXXXXXX, _XXXXXXX, ___XX___, _X_XX___, __XX____, XX_XX___, _X_XX___, ___XXXXX, ________,
  _XXX_XXX, XX__XXXX, __X__XXX, __X__XXX, XX_X__XX, _XXXXX_X, XXXXXX_X, XXXXXXXX, X_______, ___XXXXX, XXXXXXXX, _XXXXXXX, ___XX___, ___XX___, __XX____, XX_XX___, ___XX___, ___XXXXX, ________,
  _XXX_XXX, XX__XXXX, __X__XXX, __X_____, _X_XX__X, _XXXXX_X, XXXXXX_X, XXXXXXXX, X_______, _XXXXXXX, XXXXXXXX, _XXXXXXX, ___XX___, ___XX___, __XX____, XX__XXXX, X__XXXXX, X__XXXXX, ________,
  _XXX_XXX, XX__XXXX, __X_____, _XX__XXX, XX_XX__X, _XXXXX_X, XXXXXX_X, XXXXXXXX, X___XXX_, __XXXXXX, XXXXXXXX, _XXXXXXX, ___XX___, ___XX___, __XX____, XX______, XX_XX___, ___XXXXX, ________,
  _XXX_XXX, XX__XXXX, __X__XXX, XXX__XXX, XX_XXX__, _XXXXX_X, XXXXXX_X, XXXXXXXX, X___XXXX, ___XXXXX, XXXXXXXX, _XXXXXXX, ___XX___, _X_XX___, __XX____, XX_X____, XX_XX___, ___XXXXX, ________,
  _XXX_XXX, XX__XXXX, __X__XXX, XXX__XXX, XX_XXX__, _XXXXX_X, XXXXXX_X, XXXXXXXX, X___XXXX, ____XXXX, XXXXXXXX, _XXXXXXX, ___XX___, _X_XX___, __XX____, XX_X____, XX_XX___, ___XXXXX, ________,
  _XXX_XXX, XXX_____, _XX__XXX, XXX_____, _X_XXXX_, _XXXXX_X, XXXXXX_X, XXXXXXXX, X___XXXX, X___XXXX, XXXXXXXX, _XXXXXXX, ____XXXX, X__XXXXX, X__XXXXX, X___XXXX, X__XXXXX, X__XXXXX, ________,
  _XXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XXXXXXX_, XXXXXXXX, X___XXXX, XX___XXX, XXXXXXX_, XXXXXXXX, ________, ________, ________, ________, ________, ___XXXXX, ________,
  _XXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XXXXXXX_, XXXXXXXX, X___XXXX, XX____XX, XXXXXXX_, XXXXXXXX, ________, ________, ________, ________, ________, ___XXXXX, ________,
  _XXX_XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XXXXXXX_, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, XXXXXXXX, ________, ________, ________, ________, ________, ___XXXXX, ________,
  _XXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX_X, XXXXXXXX, X_______, ________, ________, ________, ________, __XXXXXX, ________,
  _XXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, X_XXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, X_______, ________, ________, ________, ________, __XXXXXX, ________,
  _XXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, X_______, ________, ________, ________, ________, __XXXXXX, ________,
  _XXXXX_X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XXX, XXXXXXXX, XXX___XX, XXXXXXXX, XXXXXXXX, X___XXXX, XXXXXXXX, XX______, ________, ________, ________, ________, _XXXXXXX, ________,
  _XXXXXX_, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXX_XXXX, XXXXXXXX, XXXXXX__, ________, ________, _XXXXXXX, XXXXXXXX, XXX_____, ________, ________, ________, ________, XXXXXXXX, ________,
  _XXXXXXX, _XXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX____, ________, ________, ________, _______X, XXXXXXXX, ________,
  _XXXXXXX, X___XXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_, __XXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX___, ________, ________, ________, ______XX, XXXXXXXX, ________,
  _XXXXXXX, XXXX____, ________, ________, _______X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, ________, ________, ________, ___XXXXX, XXXXXXXX, ________
};

GUI_CONST_STORAGE GUI_BITMAP bmvv_view_close = {
  145, /* XSize */
  32, /* YSize */
  19, /* BytesPerLine */
  1, /* BitsPerPixel */
  acvv_view_close,  /* Pointer to picture data (indices) */
  &Palvv_view_close  /* Pointer to palette */
};

/* *** End of file *** */