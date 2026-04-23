extends NodeState

var dia

@export var character: CharacterBody3D
@export var animated_sprite: AnimationPlayer

@onready var idle_state_timer: Timer = Timer.new()
@export var idle_state_time_interval: float = 5.0
var idle_state_timeout: bool = false

func _ready() -> void:
	dia = get_tree().get_first_node_in_group("Dialogue")
	
	idle_state_timer.wait_time = idle_state_time_interval
	idle_state_timer.timeout.connect(on_idle_state_timeout)
	add_child(idle_state_timer)

func _on_process(_delta : float) -> void:
	pass
	
func _on_physics_process(_delta : float) -> void:
	pass

func _on_next_transitions() -> void:
	if idle_state_timeout == true && !dia.running:
		transition.emit("Walk")

func _on_enter() -> void:
	animated_sprite.play("idle")
	
	idle_state_timeout = false
	idle_state_timer.start()

func _on_exit() -> void:
	#animated_sprite.stop()
	idle_state_timer.stop()

func on_idle_state_timeout() -> void:
	idle_state_timeout = true
