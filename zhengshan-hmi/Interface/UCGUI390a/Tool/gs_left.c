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
* Source file: gs_left
* Dimensions:  55 * 31
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

static GUI_CONST_STORAGE GUI_COLOR Colorsgs_left[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE Palgs_left = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &Colorsgs_left[0]
};

static GUI_CONST_STORAGE unsigned char acgs_left[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XX______, _XXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, X_XXXXXX, X___XXXX, XXXXXXXX, XXXXXXX_,
  XXXX____, ________, ________, _XXXXXXX, XXXX_XXX, XXXXXXXX, XXXXXXX_,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXX____, _____XX_,
  XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXX__, _______X, XXXX_XX_,
  XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXX__XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXX__, ________, ___XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXX_, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXX_, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXX_, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, _____XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXX_XXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XX_XXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XX___XXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXXX_X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXX_XX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXX__X, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXXXX_, ____XXXX, XXX_____, _______X, XXXX_XX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXX____, ___XXXXX, XXXX____, _____XX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_
};

GUI_CONST_STORAGE GUI_BITMAP bmgs_left = {
  55, /* XSize */
  31, /* YSize */
  7, /* BytesPerLine */
  1, /* BitsPerPixel */
  acgs_left,  /* Pointer to picture data (indices) */
  &Palgs_left  /* Pointer to palette */
};

/* *** End of file *** */