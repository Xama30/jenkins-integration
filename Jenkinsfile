pipeline {
    agent any

    environment {
        DOCKER_IMAGE = "serveurtracker-api"
    }

    stages {
        stage('Restore & Lint') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release --no-restore'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test --no-build --configuration Release --verbosity normal'
            }
        }

        stage('Docker Build') {
            steps {
                sh "docker build -t ${DOCKER_IMAGE}:latest ./ServeurTracker.Api"
            }
        }
    }
}