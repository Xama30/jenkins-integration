pipeline {
    agent any

    options {
        githubProjectProperty(projectUrlStr: 'https://github.com/Xama30/jenkins-integration.git')
    }

    environment {
        DOCKER_IMAGE = "serveurtracker-api"
    }

    stages {
        stage('Restore & Lint') {
            steps {
                // Notifie le début de l'étape
                githubNotify context: '1. Restore & Lint', status: 'PENDING', description: 'Restauration en cours...'
                sh 'dotnet restore'
            }
            post {
                success {
                    // Notifie le succès
                    githubNotify context: '1. Restore & Lint', status: 'SUCCESS', description: 'Restauration terminée'
                }
                failure {
                    // Notifie l'échec
                    githubNotify context: '1. Restore & Lint', status: 'FAILURE', description: 'Échec de la restauration'
                }
            }
        }

        stage('Build') {
            steps {
                githubNotify context: '2. Build', status: 'PENDING', description: 'Compilation en cours...'
                sh 'dotnet build --configuration Release --no-restore'
            }
            post {
                success {
                    githubNotify context: '2. Build', status: 'SUCCESS', description: 'Compilation réussie'
                }
                failure {
                    githubNotify context: '2. Build', status: 'FAILURE', description: 'Échec de la compilation'
                }
            }
        }

        stage('Test') {
            steps {
                githubNotify context: '3. xUnit Tests', status: 'PENDING', description: 'Exécution des tests...'
                sh 'dotnet test --no-build --configuration Release --verbosity normal'
            }
            post {
                success {
                    githubNotify context: '3. xUnit Tests', status: 'SUCCESS', description: 'Tous les tests ont réussi'
                }
                failure {
                    githubNotify context: '3. xUnit Tests', status: 'FAILURE', description: 'Des tests ont échoué'
                }
            }
        }

        stage('Docker Build') {
            steps {
                githubNotify context: '4. Docker Image', status: 'PENDING', description: 'Génération de l\'image...'
                sh "docker build -t ${DOCKER_IMAGE}:latest ./ServeurTracker.Api"
            }
            post {
                success {
                    githubNotify context: '4. Docker Image', status: 'SUCCESS', description: 'Image Docker générée'
                }
                failure {
                    githubNotify context: '4. Docker Image', status: 'FAILURE', description: 'Échec de la génération Docker'
                }
            }
        }
    }
}