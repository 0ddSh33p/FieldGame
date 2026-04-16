extends CharacterBody3D
@onready var path_follow: PathFollow3D = $Path3D/PathFollow3D
@onready var path: Path3D = $Path3D

@export var move_speed: float = 0.01
var last_position: Vector3

func _ready() -> void:
	position = path_follow.global_position
	last_position = position

func _physics_process(delta: float) -> void:
	path_follow.progress += move_speed * delta
	position = path_follow.global_position
	last_position = position
