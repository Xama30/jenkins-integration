pipeline {
    agent any

    environment {
        // On définit des variables pour simplifier le script
        DOCKER_IMAGE = "serveurtracker-api"
    }

    stages {
        stage('Checkout') {
            steps {
                // Jenkins récupère ton code depuis Git
                checkout scm
            }
        }

        stage('Restore & Lint') {
            steps {
                echo 'Vérification du formatage et restauration des paquets...'
                // On restaure les paquets
                sh 'dotnet restore'
                // Optionnel : Si tu as installé dotnet-format, tu peux vérifier le Lint ici
                // sh 'dotnet format --verify-no-changes' 
            }
        }

        stage('Build') {
            steps {
                echo 'Compilation de la solution...'
                sh 'dotnet build --configuration Release --no-restore'
            }
        }

        stage('Test') {
            steps {
                echo 'Exécution des tests unitaires xUnit...'
                // On lance les tests. Si un test échoue, le pipeline s'arrête ici !
                sh 'dotnet test --no-build --configuration Release --verbosity normal'
            }
        }

        stage('Docker Build') {
            steps {
                echo 'Création de l image Docker...'
                // On utilise le Dockerfile qu'on a créé ensemble
                sh "docker build -t ${DOCKER_IMAGE}:latest ./ServeurTracker.Api"
            }
        }
    }

    post {
        success {
            echo 'Félicitations ! Le pipeline est passé, les tests sont au vert. ✅'
        }
        failure {
            echo 'Le build a échoué. Vérifie les logs des tests ou de la compilation. ❌'
        }
    }
}