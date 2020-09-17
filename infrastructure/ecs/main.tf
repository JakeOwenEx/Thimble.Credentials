variable "service_name" {}
variable "vpc_id" {}
variable "Subnet_Id_A" {}
variable "Subnet_Id_B" {}

resource "aws_ecs_cluster" "ecs_cluster" {
    name = "${var.service_name}-cluster"
}

resource "aws_lb" "lb" {
    name = "${var.service_name}-loadbalancer"

    subnets = [
        "${var.Subnet_Id_A}", 
        "${var.Subnet_Id_B}"
    ]

    security_groups = ["${aws_security_group.lbSecurityGroup.id}"]
}

resource "aws_lb_target_group" "lbTargetGroup" {
  name = "${var.service_name}-lb-targetgroup"
  vpc_id = "${var.vpc_id}"
  protocol = "HTTP"
  port = "80"
}


resource "aws_lb_listener" "lbListener" {
  load_balancer_arn = "${aws_lb.lb.arn}"
  port = "80"
  protocol = "HTTP"
  default_action {
    type             = "forward"
    target_group_arn = "${aws_lb_target_group.lbTargetGroup.arn}"
  }
}


resource "aws_security_group" "lbSecurityGroup" {
  name = "${var.service_name}-lb-securitygroup"
  vpc_id = "${var.vpc_id}"
  
  ingress {
    cidr_blocks = ["0.0.0.0/0"]
    protocol = "-1"    
    from_port = 0
    to_port = 0
  }
  egress {
  from_port   = 0
  to_port     = 0
  protocol    = "-1"
  cidr_blocks = ["0.0.0.0/0"]
}
}

resource "aws_cloudwatch_log_group" "ecsLogs" {
  name = "${var.service_name}-logs"
  retention_in_days = 1
}

resource "aws_security_group" "EcsSecurityGroup" {
  name = "${var.service_name}-ecs-securitygroup"
  vpc_id = "${var.vpc_id}"
  
  ingress {
    cidr_blocks = ["0.0.0.0/0"]
    protocol = "-1"
    security_groups = ["${aws_security_group.lbSecurityGroup.id}"]
    from_port = 0
    to_port = 0
  }
  egress {
  from_port   = 0
  to_port     = 0
  protocol    = "-1"
  cidr_blocks = ["0.0.0.0/0"]
}
}


module "iamrole" {
  source = "../iam" 
}

module "api" {
  source = "../api"
  service_name = "${var.service_name}"
  lb_listener_arn = "${aws_lb_listener.lbListener.arn}"
  vpc_id = "${var.vpc_id}"
  ECSExecRole = "${module.iamrole.role_arn}"
  target_group_arn = "${aws_lb_target_group.lbTargetGroup.arn}"
  ecs_cluster_id = "${aws_ecs_cluster.ecs_cluster.id}"
  subnet_a_id  = "${var.Subnet_Id_A}"
  subnet_b_id  = "${var.Subnet_Id_B}"
  container_security_group = "${aws_security_group.EcsSecurityGroup.id}"
}

