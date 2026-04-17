extends CharacterBody3D
@onready var path_follow: PathFollow3D = $Path3D/PathFollow3D
@onready var path: Path3D = $Path3D

@export var move_speed: float = 0.01
@export var npc_group : String
@export var my_dialogue : Array[String]
@export var dialogue_chunk_lengths : Array[int]

var dio;
var run_dia = false
var cur_dia = 0;
var dia_entry_id = 0;
var last_position: Vector3

func _ready() -> void:
	position = path_follow.global_position
	last_position = position
	dio = get_tree().get_first_node_in_group("Dialogue")

func _physics_process(delta: float) -> void:
	path_follow.progress += move_speed * delta
	#global_position = path_follow.global_position
	last_position = position

func _process(_delta: float) -> void:
	if run_dia && Input.is_action_just_pressed("talk") && !dio.running:
		dio.openDiologue(npc_group, my_dialogue, cur_dia, cur_dia + dialogue_chunk_lengths[dia_entry_id]);
		cur_dia += dialogue_chunk_lengths[dia_entry_id]
		if (dia_entry_id < dialogue_chunk_lengths.size() -1):
			dia_entry_id += 1
		run_dia = false;

func _on_area_3d_body_entered(body: Node3D) -> void:
	if body.is_in_group("Player"):
		run_dia = true;


func _on_area_3d_body_exited(body: Node3D) -> void:
	if body.is_in_group("Player"):
		run_dia = false;
