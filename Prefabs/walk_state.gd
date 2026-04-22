extends NodeState

@export var character: CharacterBody3D
@export var animated_sprite: AnimatedSprite3D
@export var navigation_agent: NavigationAgent3D
@export var min_speed: float = 0.5
@export var max_speed: float = 1

var speed: float

func _ready() -> void:
	# call char setup after current frame finishes porcessing
	call_deferred("character_setup")

func character_setup() -> void:
	# wait to process next target
	await get_tree().physics_frame
	
	set_movement_target()

func set_movement_target() -> void:
	var target_position: Vector3 = NavigationServer3D.map_get_random_point(navigation_agent.get_navigation_map(), navigation_agent.navigation_layers, false)
	navigation_agent.target_position = target_position
	speed = randf_range(min_speed, max_speed)
	

func _on_process(_delta : float) -> void:
	pass

func _on_physics_process(_delta : float) -> void:
	if navigation_agent.is_navigation_finished():
		set_movement_target()
		return
		
	var target_position: Vector3 = navigation_agent.get_next_path_position()
	var target_direction: Vector3 = character.global_position.direction_to(target_position)
	character.velocity = target_direction * speed
	character.move_and_slide()


func _on_next_transitions() -> void:
	if navigation_agent.is_navigation_finished():
		character.velocity = Vector3.ZERO
		transition.emit("idle")


func _on_enter() -> void:
	animated_sprite.play("walk")


func _on_exit() -> void:
	animated_sprite.stop()
	print("walk")
