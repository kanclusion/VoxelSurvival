shader_type spatial;

render_mode unshaded, cull_front, depth_draw_opaque;

uniform vec4 outline_color : source_color = vec4(1.0, 0.85, 0.0, 1.0);
uniform float outline_width : hint_range(0.0, 10.0) = 2.0;

void vertex()
{
    // Положение вершины в clip space
    vec4 clip_pos = PROJECTION_MATRIX * MODELVIEW_MATRIX * vec4(VERTEX, 1.0);

    // Нормаль в clip space
    vec3 clip_normal = mat3(PROJECTION_MATRIX) * (mat3(MODELVIEW_MATRIX) * NORMAL);

    vec2 dir = clip_normal.xy;
    float len = length(dir);

    if (len > 0.00001)
        dir /= len;
    else
        dir = vec2(0.0);

    // Смещение в пикселях
    vec2 offset = dir * outline_width * clip_pos.w / VIEWPORT_SIZE * 2.0;

    clip_pos.xy += offset;

    POSITION = clip_pos;
}

void fragment()
{
    ALBEDO = outline_color.rgb;
    ALPHA = outline_color.a;
}