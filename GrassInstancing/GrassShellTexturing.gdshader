shader_type spatial;
//render_mode shadows_disabled;

uniform vec2 size;
uniform vec2 offset;
uniform float height;
uniform vec3 color;
uniform float circle_radius : hint_range(0.0, 0.5) = 0.4; // relative to cell size (0.5 is max to stay inside)

float hash(uint n) {
	n = (n << 13U) ^ n;
	n = n * (n * n * 15731U + 0x789221U) + 0x13763125U;
	return float(n & uint(0x7fffffffU)) / float(0x7fffffff);
}

void fragment() {
	ALPHA_SCISSOR_THRESHOLD = 1.0;
	vec2 grid_pos = UV * size + offset;

		uvec2 cell_id = uvec2(floor(grid_pos));

	// Brick offset: offset x by 0.5 for every odd row
	if (cell_id.y % 2u == 1u) {
		grid_pos.x += 0.5;
	}

	// Recalculate after brick offset
	cell_id = uvec2(floor(grid_pos));
	uint seed = cell_id.x + 100U * cell_id.y + 100U * 10U;
	float noise = hash(seed);

	// Position within the cell, range [0,1)
	vec2 local_pos = fract(grid_pos);

	// Distance from the center of the cell (0.5,0.5)
	float dist = distance(local_pos, vec2(0.5));

	if (noise > height && dist <= circle_radius) {
		ALPHA = 1.0;
	} else {
		ALPHA = 0.0;
	}
	ALBEDO = color;
}
