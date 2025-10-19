function scr_craft_alchemy_base()
{
    var _target = noone
    var _owner = o_witcherAlchemyCraftingMenu.id
    var items_to_check = argument0
    var _size = array_length(items_to_check)

    var _craft = false
    if (!is_undefined(argument2))
        _craft = argument2

    if (!_craft)
    {
        for (var i = 0; i < _size; i += 2)
        {
            var _item = items_to_check[i]
            var _need_count = items_to_check[i + 1]
            var _items_count = 0

            with (_item)
            {
                if (owner == _owner)
                    _items_count++
            }

            if (_items_count >= _need_count)
                return true
        }
    }
    else
    {
        for (var i = 0; i < _size; i += 2)
        {
            var _item = items_to_check[i]
            var _need_count = items_to_check[i + 1]
            var _items_count = 0

            with (_item)
                if (owner == _owner)
                    _items_count++

            if (_items_count >= _need_count)
            {
                with (_item)
                {
                    if (owner == _owner)
                    {
                        if (_need_count > 0)
                        {
                            instance_destroy()
                            _need_count--
                        }
                    }
                }

                with (scr_inventory_add_item(argument1, id, noone, true, noone, false))
                {
                    _target = id

                    if (!scr_inventory_add(other.id, id, [other.consumsContainer]))
                    {
                        forced_drop = true
                        event_user(15)
                    }
                }

                return _target
            }

        }
    }

    return _target
}