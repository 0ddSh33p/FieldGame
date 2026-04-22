extends CharacterBody3D
@onready var path_follow: PathFollow3D = $Path3D/PathFollow3D
@onready var path: Path3D = $Path3D

@export var move_speed: float = 0.01

#a better develpoer than me would make a ll this a custom file like in the diologue manager add on, but I'm lazy
@export var npc_group : String
@export var my_dialogue : Array[String]
@export var dialogue_chunk_lengths : Array[int]
@export var bias_level : Array [int]
@export var global_id : Array [int]
@export var required_ids : Array[int]

@export var ico : Label3D;

var dio;
var run_dia = false
var updatedLast = true;
var bufferedRunningCheck = false;
var cur_dia = 0;
var dia_entry_id = 0;

func _ready() -> void:
	ico.hide()
	dio = get_tree().get_first_node_in_group("Dialogue")

func _process(_delta: float) -> void:
	if run_dia && Input.is_action_just_pressed("talk") && bufferedRunningCheck:

		print(cur_dia)

		bufferedRunningCheck = false; 
		ico.hide()

		if(!updatedLast):
			if (dia_entry_id < dialogue_chunk_lengths.size() -1 && ( dia_entry_id == dialogue_chunk_lengths.size()-1 || required_ids[dia_entry_id+1] < 0 || dio.unlockedIdFetch().has(required_ids[dia_entry_id+1]))):
				cur_dia += dialogue_chunk_lengths[dia_entry_id]
				dia_entry_id += 1
				updatedLast = true;

		dio.openDiologue(npc_group, my_dialogue, cur_dia, cur_dia + dialogue_chunk_lengths[dia_entry_id], bias_level, global_id);
		
		if (dia_entry_id < dialogue_chunk_lengths.size() -1 && ( dia_entry_id == dialogue_chunk_lengths.size()-1 || required_ids[dia_entry_id+1] < 0 || dio.unlockedIdFetch().has(required_ids[dia_entry_id+1]))):
			cur_dia += dialogue_chunk_lengths[dia_entry_id]
			dia_entry_id += 1
			updatedLast = true;
		else:
			updatedLast = false
		

	if !dio.running:
		bufferedRunningCheck = true;
		if(run_dia):
			ico.show()

func _on_area_3d_body_entered(body: Node3D) -> void:
	if body.is_in_group("Player"):
		run_dia = true;
		

func _on_area_3d_body_exited(body: Node3D) -> void:
	if body.is_in_group("Player"):
		run_dia = false;
		ico.hide()
