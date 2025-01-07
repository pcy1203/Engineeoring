<a href="https://play.unity.com/en/games/3540025d-1439-4372-8b48-751ac7f4571f/engineeoring" target="_blank">
  <img src="https://github.com/user-attachments/assets/2fb6cc76-eb29-4459-a593-cedc4cceb718" alt="배너" width="100%"/>
</a>

<br/>
<hr />
<br/>

# 🌟 1. Project Overview (프로젝트 개요)
- **프로젝트 이름** : 엔지니오링(Engineeoring)
- **프로젝트 설명** : 3D Auto-Runner 게임 (서울대학교 자하연에서 출발한 오리의 공과대학 302동까지의 여정)
- **프로젝트 기간** : 2024/09/25 ~ 2024/12/20
- **비고** : 2024-2학기 서울대학교 컴퓨터공학부 '소프트웨어 개발의 원리와 실습' 과목 팀 프로젝트
<br/>
<br/>


# 🌟 2. Team Members (팀원 소개)
| 이종인 | 김형준 | 박찬영 | 전수빈 | 최재웅 |
|:------:|:------:|:------:|:------:|:------:|
| PM | Unity | Unity | Blender | Blender |
|태스크/스크럼 관리<br/>그래픽/사운드 디자인|프로그래밍<br/>게임 레벨 기획|프로그래밍<br/>비주얼 이펙트 디자인|그래픽/UI 디자인<br/>QA 담당|그래픽 디자인<br/>QA 담당|
<br/>
<br/>


# 🌟 3. Game Introduction (게임 소개)
- **게임 스토리** :
  - 게임의 배경은 서울대학교 자하연에서 출발하여 공과대학 302동까지의 여정을 다룬다. 자하연에 살고 있는 오리는 어느 날 우연히 302동에 계신 전설적인 프로그래밍 교수님에 대한 소문을 듣게 되고, 프로그래밍을 배우기 위해 모험을 결심한다. 평화로운 자하연을 떠나 험난한 캠퍼스를 거쳐 302동에 도달하기까지 오리는 다양한 장애물과 맞서게 된다. 평화로운 자하연을 떠나 험난한 캠퍼스를 거쳐 302동에 도달하려는 오리의 모험은 꿈을 향한 도전과 성장을 의미한다.
    
- **스테이지 및 장애물** :
  - **스테이지 1 (자하연)** : 자하연에서 평화로운 일상을 보내고 있던 오리는 전설적인 프로그래밍 교수님에 대한 이야기를 듣고 흥미를 느끼게 되고, 이에 따라 자하연에서 벗어나 302동으로 향하고자 한다. 이 과정에서 고양이, 족제비와 같은 동물들(점프 및 좌우 회피), 나무(슬라이드 회피)와 같은 장애물들을 마주한다.

  - **스테이지 2 (학생식당)** : 자하연을 탈출한 오리는 교수님이 학생식당에 자주 오신다는 소문을 듣게 되고, 교수님을 뵙기 위해 학생식당으로 향한다. 그러나 교수님은 계시지 않았고, 우연히 교수님의 제자를 만나 힌트를 얻는다. 학생식당을 배회하며 바나나껍질과 국물 같은 식재료(좌우 회피), 컨베이어 벨트(점프 회피)와 급식 카트(슬라이드 회피)을 장애물로서 마주한다.

  - **스테이지 3 (302동)** : 학생식당에서 교수님을 만나지 못한 오리는 직접 302동으로 가야 한다는 결심을 하게 된다. High-tech 콘셉트와 공과대학의 전형적인 모습이 공존하는 302동에서 이동하는 로봇(슬라이드 회피), 전공책(점프 회피), 대학원생(좌우 회피) 등을 교수님께 도달하기 이전의 장애물로서 마주한다.
 
  - 각 스테이지는 N개의 하드 코딩된 모듈이 존재하여 해당 모듈 단위로 랜덤하게 장애물 및 아이템이 등장한다.
 
  |Stage 1|Stage 2|Stage 3|
  |:--:|:--:|:--:|
  |![image](https://github.com/user-attachments/assets/7ea6fc0d-b3ba-4acb-86a0-d5a25e8cbe01)|![image](https://github.com/user-attachments/assets/d9ab2468-1a41-4ab1-b2f6-c61f1ca65776)|![image](https://github.com/user-attachments/assets/232e748f-8d25-49ef-a076-fb5b6ba202f7)|

    
- **게임 조작 방법 및 목표** :
  - **조작 방법** : 상하좌우 혹은 WASD 키를 사용하여 조작한다. 상/W 키로 점프, 하/S 키로 슬라이드, 좌우/AD 키로 좌우 이동이 가능하다. (좌우로는 5개의 칸 위에서 이동할 수 있다.)

  - **게임 목표** : 최대한 고득점으로 주어진 스테이지를 끝까지 클리어하는 것이다. 맵 내에 배치된 데자와 통해 득점할 수 있다.
  
  - **게임 오버** : 장애물에 부딪히는 경우 스테이지의 처음으로 돌아가 게임을 다시 시작할 수 있다. (이때 데자와 점수는 스테이지 시작 시의 점수로 초기화된다.)
 
  - **설정 탭** : 설정 탭을 통해 조작키 및 음량 변경이 가능하다.

- **게임 아이템**:

  |아이템|이름|효과|
  |:--:|:--:|:--:|
  |<img src="https://github.com/user-attachments/assets/7df99966-3969-4500-8621-a970addc4d29" width=70 />|**데자와**|점수 획득|
  |<img src="https://github.com/user-attachments/assets/6fcc7cd6-9f1e-4304-b6e6-00873a4fa3e6" width=80 />|**오리부스트**|2초 동안 속도 1.5배 및 장애물 무시|
  |<img src="https://github.com/user-attachments/assets/7cdac673-2ce9-414e-81cd-f3a2351dbb13" width=50 />|**벼락치기**|가까운 거리에 있는 장애물 파괴|
  |<img src="https://github.com/user-attachments/assets/46716889-5b59-476b-885f-bc24cb18153f" width=80 />|**오리 날다**|3초 동안 이단 점프 및 점프 중 좌우 컴트롤 가능|
  |<img src="https://github.com/user-attachments/assets/76ce2804-f924-4192-9fa8-bfdf1b506a5a" width=80 />|**곱빼기**|3초 동안 데자와 점수 2배 획득|

- **보다 자세한 기획 내용은 Wiki 탭에서 확인 가능함**
<br/>
<br/>


# 🌟 4. Tasks & Responsibilities (작업 및 역할 분담)

- **Sprint 0 (~ 10/22)**

  ![image](https://github.com/user-attachments/assets/6fd1456a-7410-40ea-8000-ded109d1e2d3)

- **Sprint 1 (10/23 ~ 11/5)**

  ![image](https://github.com/user-attachments/assets/8d5584ac-fadc-4c71-b674-68c01c680a54)

- **Sprint 2 (11/6 ~ 11/19)**
  
  ![image](https://github.com/user-attachments/assets/04e3bdae-a42f-4a70-b678-83983cf53002)

- **Sprint 3 (11/20 ~ 12/3)**

  ![image](https://github.com/user-attachments/assets/8fedd835-4f26-40f7-a02a-e430d38f1805)

- **Sprint 4 (12/4 ~ 12/17)**

  ![image](https://github.com/user-attachments/assets/e82592a7-2a04-4188-9ab3-04b577c3a4de)

<br/>
<br/>


# 🌟 5. Contributions & Results (수행 역할 & 결과물)

- **수행 역할** (Unity 팀원)

  - 게임 UI 디자인 및 기능 구현
  
  - 비주얼 이펙트 디자인 및 구현
 
  - 기타 게임 기능 구현 및 수정
 
  - 디자인 패턴 적용 및 문서 작성 (Sprint 2~4)
 
  - 리팩토링 진행 및 문서 작성 (Sprint 4)
 
  - 게임 배포

- **문서**
  
  - 프로젝트 관리 : [프로젝트 관리.pdf](https://github.com/user-attachments/files/18314147/default.pdf)
 
  - 게임 사용 설명서 : [게임 사용 설명서.pdf](https://github.com/user-attachments/files/18314282/default.pdf)

  |스프린트 계획 보고서|태스크 진행 요약|회고 회의 보고서|추가 요구사항|
  |:--:|:--:|:--:|:--:|
  |[Sprint 1_스프린트 계획 보고서.pdf](https://github.com/user-attachments/files/18314005/Sprint.1_.pdf)|[Sprint 1_태스크 진행 요약.pdf](https://github.com/user-attachments/files/18314006/Sprint.1_.pdf)|[Sprint 1_회고 회의 보고서.pdf](https://github.com/user-attachments/files/18314007/Sprint.1_.pdf)||
  |[Sprint 2_스프린트 계획 보고서.pdf](https://github.com/user-attachments/files/18314009/Sprint.2_.pdf)|[Sprint 2_태스크 진행 요약.pdf](https://github.com/user-attachments/files/18314010/Sprint.2_.pdf)|[Sprint 2_회고 회의 보고서.pdf](https://github.com/user-attachments/files/18314011/Sprint.2_.pdf)|[Sprint 2_디자인 패턴.pdf](https://github.com/user-attachments/files/18314008/Sprint.2_.pdf)|
  |[Sprint 3_스프린트 계획 보고서.pdf](https://github.com/user-attachments/files/18314013/Sprint.3_.pdf)|[Sprint 3_태스크 진행 요약.pdf](https://github.com/user-attachments/files/18314014/Sprint.3_.pdf)|[Sprint 3_회고 회의 보고서.pdf](https://github.com/user-attachments/files/18314016/Sprint.3_.pdf)|[Sprint 3_디자인 패턴.pdf](https://github.com/user-attachments/files/18314012/Sprint.3_.pdf)<br />[Sprint 3_테스트 문서.pdf](https://github.com/user-attachments/files/18314015/Sprint.3_.pdf)|
  |[Sprint 4_스프린트 계획 보고서.pdf](https://github.com/user-attachments/files/18314021/Sprint.4_.pdf)|[Sprint 4_태스크 진행 요약.pdf](https://github.com/user-attachments/files/18314000/Sprint.4_.pdf)|[Sprint 4_회고 회의 보고서.pdf](https://github.com/user-attachments/files/18314004/Sprint.4_.pdf)|[Sprint 4_디자인 패턴.pdf](https://github.com/user-attachments/files/18314017/Sprint.4_.pdf)<br />[Sprint 4_테스트 문서.pdf](https://github.com/user-attachments/files/18314002/Sprint.4_.pdf)<br />[Sprint 4_리팩토링 문서.pdf](https://github.com/user-attachments/files/18314020/Sprint.4_.pdf)|

<br/>
<br/>


# 🌟 6. Technology Stack (기술 스택)
## 6.1 Programming & Modeling
| Language/Tool | Version |
|:-------:|:-------:|
| ![js](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) | |
| ![js](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white) | 2021.3.52f1 |
| ![js](https://img.shields.io/badge/blender-%23F5792A.svg?style=for-the-badge&logo=blender&logoColor=white) | 4.2 |

<br/>

## 6.2 Cooperation
| Tool | Usage |
|:---:|:---:|
|![js](https://img.shields.io/badge/Slack-4A154B?style=for-the-badge&logo=slack&logoColor=white)|스크럼 관리(Geekbot) 및 소통|
|![js](https://img.shields.io/badge/Notion-000000?style=for-the-badge&logo=notion&logoColor=white)|태스크 관리 및 회의록 정리|
|![js](https://img.shields.io/badge/GIT-E44C30?style=for-the-badge&logo=git&logoColor=white)|코드 및 에셋 관리|

<br />
<hr />

<div algin="center">
(▼ 로고를 클릭하여 플레이 가능)
  <br />
  <a display="block" href="https://play.unity.com/en/games/3540025d-1439-4372-8b48-751ac7f4571f/engineeoring" target="_blank">
    <img src="https://github.com/user-attachments/assets/e94517d8-5ee2-4c15-b437-ff472dba9e5e" alt="로고" width="50%"/>
  </a>
</div>
