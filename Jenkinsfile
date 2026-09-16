pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:10.0'
        }
    }

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