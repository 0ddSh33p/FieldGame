extends CharacterBody3D
@onready var path: Path3D = $Path3D
@onready var path_follow: PathFollow3D = $Path3D/PathFollow3D

@export var moveSpeed: int = 1

var lastPosition: Vector3

func _ready() -> void:
	position = path_follow.global_position
	lastPosition = position

func _physics_process(delta: float) -> void:
	path_follow.progress += moveSpeed * delta
	position = path_follow.global_position
	lastPosition = position
