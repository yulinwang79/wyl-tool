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
* Source file: gs_valve_open_a
* Dimensions:  39 * 35
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

static GUI_CONST_STORAGE GUI_COLOR Colorsgs_valve_open_a[] = {
     0x000000,0xFFFFFF
};

static GUI_CONST_STORAGE GUI_LOGPALETTE Palgs_valve_open_a = {
  2,	/* number of entries */
  0, 	/* No transparency */
  &Colorsgs_valve_open_a[0]
};

static GUI_CONST_STORAGE unsigned char acgs_valve_open_a[] = {
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, ________, ________, _______X, XXXXXXX_,
  XXXXXX__, ________, ____XXXX, XXXXXXX_, _XXXXXX_,
  XXXXX___, ________, ____XXXX, XXXXXXXX, X_XXXXX_,
  XXXX____, ________, ____XXXX, XXXXXXXX, XX_XXXX_,
  XXX_____, ________, ____XXXX, XXXXXXXX, XXX_XXX_,
  XXX_____, ________, ____XXXX, XXXXXXXX, XXX_XXX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XX______, ________, ____XXXX, XXXXXXXX, XXXX_XX_,
  XXX_____, ________, ____XXXX, XXXXXXXX, XXX_XXX_,
  XXX_____, ________, ____XXXX, XXXXXXXX, XXX_XXX_,
  XXXX____, ________, ____XXXX, XXXXXXXX, XX_XXXX_,
  XXXXX___, ________, ____XXXX, XXXXXXXX, X_XXXXX_,
  XXXXXX__, ________, ____XXXX, XXXXXXX_, _XXXXXX_,
  XXXXXXXX, ________, ________, _______X, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_,
  XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXXX, XXXXXXX_
};

GUI_CONST_STORAGE GUI_BITMAP bmgs_valve_open_a = {
  39, /* XSize */
  35, /* YSize */
  5, /* BytesPerLine */
  1, /* BitsPerPixel */
  acgs_valve_open_a,  /* Pointer to picture data (indices) */
  &Palgs_valve_open_a  /* Pointer to palette */
};

/* *** End of file *** */
