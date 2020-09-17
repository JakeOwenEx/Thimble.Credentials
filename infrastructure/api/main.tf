variable "service_name" {}

variable "lb_listener_arn" {}

variable "vpc_id" {}

variable "ECSExecRole" {}

variable "target_group_arn" {}

variable "ecs_cluster_id" {}

variable "subnet_a_id" {}

variable "subnet_b_id" {}

variable "container_security_group" {}

resource "aws_ecs_task_definition" "ecsTaskDef" {
  family = "${var.service_name}-api"
  cpu = 256
  memory = 512
  network_mode = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  execution_role_arn = "${var.ECSExecRole}"

  container_definitions = "${file("./task-definitions/service.json")}"
}


resource "aws_lb_listener_rule" "ecsListenerRule" {
    listener_arn = "${var.lb_listener_arn}"
    priority = 2
    condition {
        path_pattern {
            values = ["*"]
        }
    }
    action {
        type             = "forward"
        target_group_arn = "${aws_lb_target_group.apiLbTargetGroup.arn}"
    }
}

resource "aws_lb_target_group" "apiLbTargetGroup" {
  name = "${var.service_name}-api-lb-targetgroup"
  vpc_id = "${var.vpc_id}"
  protocol = "HTTP"
  port = 80
  target_type = "ip"
  health_check {
      path = "/health"
      protocol = "HTTP"
      healthy_threshold = 10
      interval = 10
      timeout = 5
      matcher = "200-299"
  }
}


resource "aws_ecs_service" "ecsService" {
  name = "${var.service_name}-service"
  task_definition = "${aws_ecs_task_definition.ecsTaskDef.arn}"
  cluster = "${var.ecs_cluster_id}"
  launch_type = "FARGATE"
  desired_count = 2
  deployment_maximum_percent = 200
  deployment_minimum_healthy_percent = 70
  network_configuration {
      assign_public_ip = true
      subnets = ["${var.subnet_a_id}", "${var.subnet_b_id}"]
      security_groups = ["${var.container_security_group}"]
  }
  load_balancer {
      container_name = "${var.service_name}api"
      container_port = 80
      target_group_arn = "${aws_lb_target_group.apiLbTargetGroup.arn}"
  }
}
