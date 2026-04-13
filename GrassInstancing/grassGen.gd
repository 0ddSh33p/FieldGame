@tool
extends MeshInstance3D

@export var generate := false

@export var size := Vector2(300,300)
@export var flowerThreshold := 0.85
@export var flowerColor := Vector3(0.1,0.3,0.2)
@export var baseColor := Vector3(0.1,0.3,0.2)
@export var colorStep : Vector3
@export var numberOfSteps := 4
@export var height := 0.25
@export var heightCurve : Curve
@export var windPower := 0.002
@export var windSpeed := 0.03
@export var heightDiscrepency := 1.2

var shift = 0.0
var blades : Array[Node3D]

func _ready() -> void:
	if !Engine.is_editor_hint():
		generate = true

func _process(delta: float) -> void:
	var grass := load("res://GrassInstancing/GrassShellTexturing.gdshader")
	if generate:
		if(!blades.is_empty()):
			for blade in blades:
				blade.queue_free()
			blades.clear()
		
		for i in range(numberOfSteps):
			var grassLayerN := MeshInstance3D.new()
			var grassMesh = PlaneMesh.new()
			grassLayerN.mesh = grassMesh
			var mat = ShaderMaterial.new()
			mat.shader = grass
			mat.set_shader_parameter("size", size)
			mat.set_shader_parameter("offset", Vector2(abs(position.x),abs(position.z))*Vector2(scale.x,scale.z))
			mat.set_shader_parameter("height", heightCurve.sample(float(i)/numberOfSteps))
			if heightCurve.sample(float(i)/numberOfSteps) < flowerThreshold:
				mat.set_shader_parameter("color", baseColor + (colorStep*i))
			else:
				mat.set_shader_parameter("color", flowerColor)
			grassLayerN.material_override = mat
			grassLayerN.position = Vector3(0.0,height/float(numberOfSteps+1) * (i+1),0.0)
			grassLayerN.cast_shadow = 0;
			self.add_child(grassLayerN)
			blades.append(grassLayerN)
		generate = false
	
	if(!blades.is_empty()):
		for i in range(blades.size()):
			shift += windSpeed * delta
			var xWindOffset = sin(shift + (heightDiscrepency*i))
			var zWindOffset = sin(shift + (heightDiscrepency*i))
			
			xWindOffset *= windPower * (float(i)/blades.size()) * .1
			zWindOffset *= windPower * (float(i)/blades.size()) * .1
			
			blades[i].position.x  = xWindOffset
			blades[i].position.z = zWindOffset
