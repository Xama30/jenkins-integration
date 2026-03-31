pipeline {
    agent any

    environment {
        DOCKER_IMAGE = "serveurtracker-api"
    }

    stages {
        stage('Restore & Lint') {
            steps {
                // Announce to GitHub that this specific stage is starting
                publishChecks name: "1. Restore & Lint", status: "IN_PROGRESS"
                sh 'dotnet restore'
            }
            post {
                success {
                    publishChecks name: "1. Restore & Lint", status: "COMPLETED", conclusion: "SUCCESS"
                }
                failure {
                    publishChecks name: "1. Restore & Lint", status: "COMPLETED", conclusion: "FAILURE"
                }
            }
        }

        stage('Build') {
            steps {
                publishChecks name: "2. Build", status: "IN_PROGRESS"
                sh 'dotnet build --configuration Release --no-restore'
            }
            post {
                success {
                    publishChecks name: "2. Build", status: "COMPLETED", conclusion: "SUCCESS"
                }
                failure {
                    publishChecks name: "2. Build", status: "COMPLETED", conclusion: "FAILURE"
                }
            }
        }

        stage('Test') {
            steps {
                publishChecks name: "3. xUnit Tests", status: "IN_PROGRESS"
                sh 'dotnet test --no-build --configuration Release --verbosity normal'
            }
            post {
                success {
                    publishChecks name: "3. xUnit Tests", status: "COMPLETED", conclusion: "SUCCESS"
                }
                failure {
                    publishChecks name: "3. xUnit Tests", status: "COMPLETED", conclusion: "FAILURE"
                }
            }
        }

        stage('Docker Build') {
            steps {
                publishChecks name: "4. Docker Image", status: "IN_PROGRESS"
                sh "docker build -t ${DOCKER_IMAGE}:latest ./ServeurTracker.Api"
            }
            post {
                success {
                    publishChecks name: "4. Docker Image", status: "COMPLETED", conclusion: "SUCCESS"
                }
                failure {
                    publishChecks name: "4. Docker Image", status: "COMPLETED", conclusion: "FAILURE"
                }
            }
        }
    }
}