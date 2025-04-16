if coatingOilHeight
{
    scr_drawText(contentX, contentY + _offsetY, coatingOilText, coatingOilColor, fa_left, fa_top, global.f_dmg, textScale)
    scr_drawText(contentX + contentWidth, contentY + _offsetY, coatingOilRight, coatingOilColor, fa_right, fa_top, global.f_dmg, textScale)
    _offsetY += coatingOilHeight
}
