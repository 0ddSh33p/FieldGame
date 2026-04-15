extends Node
var group: String

@export var dialoguePaths: Array[Resource]
@export var repeatLastDialogue: bool = false

var currDialogue: int = 0
var balloon: Resource
var noMoreDialogue: bool = false

func _ready() -> void:
	group = get_parent().group
	# set dialogue balloon variant depending on type of npc
	if group == "grass":
		balloon = load("res://Dialogue/Balloons/grass_npc_balloon.tscn")
	elif group == "rock":
		balloon = load("res://Dialogue/Balloons/rock_npc_balloon.tscn")
	else:
		# default
		balloon = load("res://addons/dialogue_manager/example_balloon/example_balloon.tscn")

func _on_area_3d_body_entered(body: Node3D) -> void:
	if body.name == "Player":
		# ran out of dialogue
		if noMoreDialogue && !repeatLastDialogue:
			return
		
		# dialogue start
		DialogueInfo.dialogueRunning = true
		DialogueManager.show_dialogue_balloon_scene(balloon, dialoguePaths[currDialogue], "start")
		await DialogueManager.dialogue_ended
		DialogueInfo.dialogueRunning = false
		# dialogue complete
		
		# check for more dialogue
		# continue without incrementing to repeat dialogue
		if currDialogue >= dialoguePaths.size() - 1:
			noMoreDialogue = true
			return
			
		currDialogue += 1
