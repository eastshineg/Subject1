환경
=
unity 2022.3.52f

과제1
=
Summary
-
![image](https://github.com/user-attachments/assets/e79c5088-bffa-47e2-a670-fe4b932d38d7)
- **플레이**
  - Assets/Scene의 subject.scene을 open한다음 play
  - 캐릭터 역할을 하는 object (box)와 아이템 역할을 하는 object (sphere)를 배치
  - wsad키를 통해 이동해서 충돌처리를 수행하고 이를 기반으로 속도의 변화가 수행되도록 구현
  - 과제 수행 진입점은 GameSupervisor의 Play()

- **GameSupervisor**
  - Singleton으로 작성
  - play 환경 기본 구축을 위한 시작점
  - 각 NeopleObject의 충돌 이벤트를 제어 및 NeopleObject의 생명주기를 관리 하기 위한 프로세스 구현
- **NeopleObject**
  - 서로 상호작용 및 제어를 위한 기본 class
  - **NeoplePlayerObject**
    - keyboard의 w,s,a,d 제어 및 item object와 충돌 이후에 속도가 변경되는 프로세스를 수행하기 위한 상세 class
  - **NeopleItemObject**
    - Player object에 영향을 주기 위한 정보를 가지고 있는 상세 class
- **ObjectManager**
  - NeopleObject의 생성 및 파괴를 관리하는 클래스
- **NeopleComponent**
  - NeopleObject와 실제 GameObject간의 상호작용을 하기위한 기본 Component
  - NeopleObject는 NeopleComponent를 가질 수도 있고 없을 수도 있다.
  - **NeoplePlayerComponent**
    - Collider간의 충돌을 제어해서 필요한 충돌 정보만을 취합하기 위한 상세 Component
- **NeopleColliderTrigger**
  - NeopleComponent로 부터 설정받은 정보를 기반으로 충돌정보를 전달하기 위한 Component
  - Collider간의 충돌 이벤트를 전달 받고 처리하는 로직 위주로 구성
