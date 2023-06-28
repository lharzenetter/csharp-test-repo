pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0-preview'
        }
    }

    stages {
        stage ('ls') {
            steps {
                script {
                    def output = sh(returnStdout: true, script: 'ls -al')
                    echo "Output: ${output}"
                }
            }
        }

        stage ('Build') {
            steps {
                echo "building..."
                sh 'dotnet --version'
                sleep 10
            }
        }
    }
}