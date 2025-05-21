if (instance_exists(aoe_target))
{
    scr_escapeButtonListAdd()

    with (o_skill)
        is_activate = false

    is_activate = true
    global.skill_activate = true
    var xx = scr_round_cell(aoe_target.xx)
    var yy = scr_round_cell(aoe_target.yy)

    var i_id = aoe_target
    var main_own = i_id
    var width = AOE_Len
    
    repeat (AOE_Len)
    {
        var start = i_id

        repeat (width)
        {
            // 右斜角
            with (scr_create_one_aoe(i_id, 45, main_own, is_moving))
                i_id = id
        }
        
        i_id = start
        
        repeat (width)
        {
            // 左斜角
            with (scr_create_one_aoe(i_id, -45, main_own, is_moving))
                i_id = id
        }

        i_id = start

        // 创建中间笔直的方块，每次迭代“起点”就往前进一格
        i_id = scr_create_one_aoe(i_id, 0, main_own, is_moving)
        width--
    }


    with (instance_create_depth(xx + 13, yy + 13, 0, o_startcast_loop))
    {
        startcast_sprite_tag = other.startcast_sprite_tag
        
        if (other.loop_sound != -4)
            snd_loop = scr_audio_play_at_loop(other.loop_sound)
        
        event_user(0)
    }

    var i = -range
    
    while (i <= range)
    {
        var j = -range
        
        while (j <= range)
        {
            var _targetX = xx + (i * 26) + 13
            var _targetY = yy + (j * 26) + 13
            
            if (_targetX >= 0 && _targetY >= 0 && _targetX <= room_width && _targetY <= room_height)
            {
                with (o_controller)
                    event_user(1)
                
                if (script_execute(filter, aoe_target.x, aoe_target.y, _targetX, _targetY, false, range))
                {
                    with (instance_create_depth(_targetX, _targetY, 0, o_aoe_range))
                    {
                        delete_on_line = other.delete_on_line
                        visible_check = other.visible_check
                        range = other.range
                        var _inst = scr_tile_get_instance(x, y, 0, 0)
                        
                        if (_inst && delete_on_line)
                        {
                            if (!scr_can_interract_path(_inst, other.aoe_target, other.range))
                                instance_destroy()
                        }
                        
                        event_perform(ev_step, ev_step_normal)
                        var _obj = ds_grid_get_ext(o_controller.posgrid, grid_x, grid_y)
                        
                        if (_obj && instance_exists(_obj) && _obj.object_index == o_tile_transition)
                            instance_destroy()
                    }
                }
            }
            
            j++
        }
        
        i++
    }
}