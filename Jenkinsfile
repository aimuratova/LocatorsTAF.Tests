pipeline {
    agent any

    stages {
        stage('Environment') {
            steps {
                sh 'dotnet --version'
                sh 'dotnet --info'
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