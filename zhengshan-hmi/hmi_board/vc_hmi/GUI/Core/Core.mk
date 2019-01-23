SOURCE := \
	GUI_Warn.c \
	GUI_ClearRectEx.c \
	LCDRLE8.c \
	LCD_GetColorIndex.c \
	GUI_MergeRect.c \
	GUI_CursorHeaderM.c \
	GUI_SetTextStyle.c \
	LCD_GetNumDisplays.c \
	LCD_SetClipRectEx.c \
	GUI_AddDec.c \
	GUI_GetClientRect.c \
	GUI__Read.c \
	GUIAlloc.c \
	LCDInfo.c \
	LCDColor.c \
	GUI_SetFont.c \
	GUICharP.c \
	GUI_CursorCrossMI.c \
	GUI_TOUCH_DriverAnalog.c \
	GUI_FillRectEx.c \
	GUI_CursorCrossSI.c \
	GUI_DrawBitmapExp.c \
	GUI_DrawBitmapMag.c \
	LCD_UpdateColorIndices.c \
	GUI_AddBin.c \
	GUI_CursorArrowL.c \
	GUI__ReduceRect.c \
	GUI_GetLineStyle.c \
	LCD_DrawVLine.c \
	GUI__HandleEOLine.c \
	GUI_AddDecMin.c \
	GUI_CursorCrossMPx.c \
	GUI_CursorCrossM.c \
	GUI_DrawRectEx.c \
	LCD_GetEx.c \
	GUI_CursorArrowLI.c \
	LCDRLE4.c \
	GUI_GetColor.c \
	GUI__DivideRound.c \
	LCDP565_Index2Color.c \
	LCD_DrawBitmap_565.c \
	GUI_CursorCrossLPx.c \
	GUI_SetOrg.c \
	GUI_GetVersionString.c \
	GUI2DLib.c \
	GUI__CalcTextRect.c \
	GUI_IsInFont.c \
	GUI_SetDefault.c \
	GUI_Goto.c \
	GUIPolyR.c \
	GUI_CursorArrowMI.c \
	GUI_SetLBorder.c \
	GUI_SetColor.c \
	GUI_DispStringInRectMax.c \
	GUI_BMP_Serialize.c \
	GUI_SelectLayer.c \
	GUI_SIF_Prop.c \
	GUI_FillRect.c \
	GUI_SetTextMode.c \
	GUI_DispStringInRectEx.c \
	GUI_DrawPixel.c \
	GUICore.c \
	GUI_GetStringDistX.c \
	GUI_Pen.c \
	GUI_DrawGraph.c \
	GUI_WaitKey.c \
	GUI_GetTextExtend.c \
	GUI_SetLUTEntry.c \
	GUI_PID.c \
	LCD.c \
	GUIArc.c \
	GUI_MOUSE.c \
	GUI_DispChar.c \
	GUI__memset.c \
	GUI_ALLOC_AllocInit.c \
	GUI_CursorArrowM.c \
	GUI_DrawPie.c \
	GUI_CursorHeaderMI.c \
	GUI_AddDecShift.c \
	GUI__memset16.c \
	GUI_DrawFocusRect.c \
	GUI_Exec.c \
	GUI_Log.c \
	GUI__GetFontSizeY.c \
	GUI_SetDecChar.c \
	GUI_SetPixelIndex.c \
	GUI_SetLUTColor.c \
	LCD_Rotate180.c \
	GUI_DispCEOL.c \
	GUI_CursorArrowSPx.c \
	GUI_DispStringInRect.c \
	GUI_OnKey.c \
	GUI_AddKeyMsgHook.c \
	GUI_CursorArrowMPx.c \
	GUI_UC.c \
	GUI_SaveContext.c \
	GUITimer.c \
	GUI_DispHex.c \
	GUI_ErrorOut.c \
	GUI_RectsIntersect.c \
	GUI_GetYSizeOfFont.c \
	GUIUC0.c \
	GUI_DispStringHCenter.c \
	GUI_Color2VisColor.c \
	GUI_MoveRect.c \
	GUIPolyE.c \
	GUI_CursorCrossL.c \
	LCDAA.c \
	GUI_SIF.c \
	GUI_CursorPalI.c \
	GUI_CursorArrowS.c \
	GUICirc.c \
	GUICharM.c \
	GUI_CursorArrowLPx.c \
	GUI_DispStringAtCEOL.c \
	GUI_SetDrawMode.c \
	GUI_SetLUTColorEx.c \
	GUI_DrawPolyline.c \
	GUI_AddHex.c \
	GUI__AddSpaceHex.c \
	GUI_FillPolygon.c \
	GUI_CursorCrossSPx.c \
	LCD_MixColors256.c \
	LCD_API.c \
	GUI_BMP.c \
	GUI__IntersectRects.c \
	GUI_DrawBitmap.c \
	GUIValf.c \
	GUI_GetFontInfo.c \
	LCDInfo0.c \
	GUI_GetBitmapPixelIndex.c \
	LCD_GetPixelColor.c \
	GUI_SetTextAlign.c \
	GUI__Wrap.c \
	LCD_L0_Generic.c \
	LCDGetP.c \
	LCDL0Mag.c \
	GUIEncJS.c \
	GUIPolyM.c \
	GUI__IntersectRect.c \
	GUI_InvertRect.c \
	LCDPM565_Index2Color.c \
	GUICurs.c \
	GUI_CursorPal.c \
	GUICharLine.c \
	GUI_GetFont.c \
	GUIIndex2Color.c \
	GUI__strlen.c \
	GUI_UC_EncodeUTF8.c \
	GUI_SetLineStyle.c \
	LCD_SetAPI.c \
	GUI_TOUCH.c \
	GUIChar.c \
	GUI_CursorHeaderMPx.c \
	GUI_InitLUT.c \
	LCDInfo1.c \
	LCD_Mirror.c \
	GUI_DrawBitmapEx.c \
	GUI__DivideRound32.c \
	GUITime.c \
	LCDL0Delta.c \
	GUI_CalcColorDist.c \
	GUI_UC_DispString.c \
	GUI_GetTextMode.c \
	GUIStream.c \
	LCD_Index2ColorEx.c \
	GUI_CursorCrossS.c \
	LCD_RotateCW.c \
	GUIRealloc.c \
	GUI_WaitEvent.c \
	GUI_CursorCrossLI.c \
	GUI_UC_EncodeNone.c \
	GUI__SetText.c \
	LCD_SelectLCD.c \
	GUITask.c \
	GUI__strcmp.c \
	LCDP1.c \
	GUI_DrawVLine.c \
	GUI_DispString.c \
	GUI_DispStringAt.c \
	GUIVal.c \
	GUIColor2Index.c \
	GUI_DrawHLine.c \
	GUI__GetNumChars.c \
	GUI_CursorArrowSI.c \
	GUI_MOUSE_DriverPS2.c \
	LCD_RotateCCW.c \
	GUI_GetBitmapPixelColor.c \
	GUI_DispBin.c \
	GUI_SelectLCD.c \
	GUI_DispStringLen.c \
	GUI_GetFontSizeY.c \
	GUI_GetDispPos.c \
	GUI_SetColorIndex.c \
	GUI_ALLOC_AllocZero.c \
	GUI_DispChars.c \
	LCD_DrawBitmap_M565.c \
	GUI_GetTextAlign.c