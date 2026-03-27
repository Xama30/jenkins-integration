pipeline {
    agent any

    environment {
        DOCKER_IMAGE = "serveurtracker-api"
    }

    stages {
        stage('Restore & Lint') {
            steps {
                // On annonce le début du check à GitHub
                publishChecks name: "1. Restore & Lint", status: "IN_PROGRESS"
                
                script {
                    try {
                        sh 'dotnet restore'
                        publishChecks name: "1. Restore & Lint", status: "COMPLETED", conclusion: "SUCCESS"
                    } catch (e) {
                        publishChecks name: "1. Restore & Lint", status: "COMPLETED", conclusion: "FAILURE"
                        error "Le Restore a échoué"
                    }
                }
            }
        }

        stage('Build') {
            steps {
                publishChecks name: "2. Compilation", status: "IN_PROGRESS"
                
                script {
                    try {
                        sh 'dotnet build --configuration Release --no-restore'
                        publishChecks name: "2. Compilation", status: "COMPLETED", conclusion: "SUCCESS"
                    } catch (e) {
                        publishChecks name: "2. Compilation", status: "COMPLETED", conclusion: "FAILURE"
                        error "La compilation a échoué"
                    }
                }
            }
        }

        stage('Test') {
            steps {
                publishChecks name: "3. xUnit Tests", status: "IN_PROGRESS"
                
                script {
                    try {
                        // On lance les tests
                        sh 'dotnet test --no-build --configuration Release --verbosity normal'
                        publishChecks name: "3. xUnit Tests", status: "COMPLETED", conclusion: "SUCCESS"
                    } catch (e) {
                        publishChecks name: "3. xUnit Tests", status: "COMPLETED", conclusion: "FAILURE"
                        error "Les tests ont échoué"
                    }
                }
            }
        }

        stage('Docker Build') {
            steps {
                publishChecks name: "4. Docker Image", status: "IN_PROGRESS"
                
                script {
                    try {
                        sh "docker build -t ${DOCKER_IMAGE}:latest ./ServeurTracker.Api"
                        publishChecks name: "4. Docker Image", status: "COMPLETED", conclusion: "SUCCESS"
                    } catch (e) {
                        publishChecks name: "4. Docker Image", status: "COMPLETED", conclusion: "FAILURE"
                        error "Le build Docker a échoué"
                    }
                }
            }
        }
    }

    // Sécurité globale : si le pipeline plante n'importe où
    post {
        failure {
            echo 'Pipeline interrompu. Vérifie les détails sur GitHub.'
        }
    }
}