resource "aws_iam_role" "task_exec_role" {
  name = "credentials-ecs-execrole"
  path = "/"
  assume_role_policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Action": "sts:AssumeRole",
      "Principal": {
        "Service": "ecs-tasks.amazonaws.com"
      },
      "Effect": "Allow",
      "Sid": ""
    }
  ]
}
EOF

}

resource "aws_iam_policy" "ecr_policy" {
  name="credentials-ecr-policy"
policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
{"Action": [
        "ECR:GetAuthorizationToken",
        "ECR:BatchCheckLayerAvailability",
        "ECR:GetDownloadUrlForLayer",
        "ECR:BatchGetImage",
        "LOGS:CreateLogStream",
        "LOGS:PutLogEvents"
      ],
      "Effect": "Allow",
      "Resource": "*"
    }
  ]
}
EOF
}

resource "aws_iam_role_policy_attachment" "ecr_attach" {
  role = "${aws_iam_role.task_exec_role.name}"
  policy_arn = "${aws_iam_policy.ecr_policy.arn}"
}


output "role_arn" {
    value = "${aws_iam_role.task_exec_role.arn}"
}
