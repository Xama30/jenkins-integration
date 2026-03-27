pipeline {
    agent any

    // Ce bloc 'options' est le remède miracle pour lier Jenkins à GitHub
    options {
        githubProjectProperty(projectUrlStr: 'https://github.com/Xama30/jenkins-integration/')
    }

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

    // Jenkins enverra automatiquement le statut final à GitHub
    post {
        success {
            echo 'Pipeline terminé avec succès. Les statuts sont envoyés à GitHub.'
        }
        failure {
            echo 'Pipeline interrompu. Vérifie les logs.'
        }
    }
}