# TopDownShooterTestTaskVolodymyr

This Project is a prototype for basic core loop from scratch:

Top down view with first, third views change by input command

the project has bug when active topDownView and UI input, I Need rewrite hering input from touch.

basic moving and shooter.

# Enemy:
Enemy unit has own behaviour. Main control is outside unit controller.

# Короткий опис запуску геймплею
Можна запустити сцену Bootstrap, або BootstrapScene.

Друга сцена запускає геймплей локально, і є обмеженою відносно публічних сервісів.

Перша сцена це блокування знімає наявністю сінглтона, який дозволяє читати збережені файли, впливати на налаштування графіки, і може бути містком для стану між сценами яку можна додати із наявністю ізоляції систем.

Bootstrap запускає всі системи, і завантажує головне меню, з якого можна зайти у налаштування, або запустити геймплей.

для зміни графіки UI інтерфейс налаштувань повинен мати посилання на дані, оскільки є візуальним шаром без привязки до публічних налаштувань
