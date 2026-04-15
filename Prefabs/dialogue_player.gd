extends Node
@export var dialoguePath: String
var dialogue : DialogueResource

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	dialogue = load(dialoguePath)

func _on_dialogue_trigger_area_body_entered(body: Node3D) -> void:
	if body.name == 'Player':
		# Run dialogue
		GlobalsIdk.dialogueRunning = true
		DialogueManager.show_dialogue_balloon(dialogue, "start")
		await DialogueManager.dialogue_ended
		GlobalsIdk.dialogueRunning = false
