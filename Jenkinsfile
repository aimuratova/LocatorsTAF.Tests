pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:10.0'
            args '-u root --entrypoint=""'
        }
    }

    stages {
        stage('Checkout') {
            steps {
                echo 'Getting source code from Git...'
                sh 'git clean -fdx'
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release --no-restore'
            }
        }
    }
}