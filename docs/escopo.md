# Escopo do projeto — Maçonaria

**Data:** 09/11/2026

**Disciplina:** Desenvolvimento de Jogos Digitais

**Professor:** Prof. Me. Leandro Luis Pauro

**Integrantes:** Miguel Maldonado Ferreira e Daniel Arlindo

**Referência:** Aula11.pdf, páginas 2 a 11

**Situação:** proposta de escopo; desenvolvimento ainda não iniciado.

## 1. Conceito

Maçonaria é um jogo de sobrevivência em arena 3D, com câmera elevada e ataques automáticos. O jogador controla um penitente preso no claustro de uma catedral em ruínas durante uma vigília ritual. Criaturas se aproximam continuamente, e a movimentação é o principal recurso para evitar o cerco e recolher experiência.

A referência de jogabilidade é Vampire Survivors: movimentação livre, combate automático, pressão crescente e evolução durante a partida. Blasphemous orienta a atmosfera: arquitetura religiosa decadente, figuras penitentes, vestes cerimoniais desgastadas e cores austeras. Essas referências serão traduzidas para um jogo 3D com personagens, símbolos e cenário próprios; não serão reutilizados personagens, nomes, sprites ou outros assets dos jogos de referência. O universo e a ordem ritual do jogo serão fictícios.

- **Gênero:** ação e sobrevivência em arena, no estilo survivors.
- **Plataforma prevista:** computador, com teclado e execução local.
- **Público-alvo:** jogadores que gostam de partidas curtas de ação e progressão simples.
- **Objetivo:** sobreviver por três minutos.
- **Vitória:** chegar ao fim do tempo com vida.
- **Derrota:** perder todos os pontos de vida antes do fim da partida.
- **Nome:** Maçonaria.

Os números de duração, dano, pontuação e progressão abaixo são decisões desta proposta, não exigências do PDF. Poderão ser ajustados após os primeiros testes.

## 2. Primeira versão jogável

### Arena e apresentação

Uma única arena compacta representa o claustro de uma catedral em ruínas, com arcos quebrados, lápides e um altar central. Piso, muros e poucos obstáculos delimitam o espaço. Os caminhos devem permitir circular ao redor dos inimigos sem passagens estreitas que prejudiquem a movimentação.

O cenário utilizará objetos 3D simples, materiais de pedra envelhecida e uma paleta de cinza, vinho escuro e dourado gasto. Silhuetas e efeitos luminosos deverão manter personagem, inimigos, projéteis e itens legíveis sobre o fundo escuro. Não se exige violência gráfica no protótipo; a atmosfera virá da arquitetura, das vestes e da iluminação. A câmera ficará elevada, inclinada e acompanhará o jogador, mantendo o plano de combate visível.

Para o protótipo, modelos simples feitos com primitivas da Unity são suficientes. Assets gratuitos poderão substituir esses modelos, desde que tenham licença adequada e não atrasem a montagem do cenário.

### Personagem e controles

O protagonista será um penitente de capuz baixo, máscara de ferro lisa e vestes cerimoniais gastas. Um relicário preso ao peito emitirá os projéteis automáticos. O inimigo será um devoto corrompido, de postura curvada, rosto coberto por tecido e mãos alongadas. As duas silhuetas deverão ser distintas e poderão ser construídas com formas geométricas na primeira versão. Essas descrições orientam futuros esboços; nenhum modelo ou desenho faz parte desta entrega de escopo.

- Movimentação por WASD e setas, no plano horizontal.
- Velocidade diagonal igual à velocidade nos demais sentidos.
- Vida inicial de três pontos.
- Um segundo de invulnerabilidade após receber dano, com indicação visual.
- Reinício pela tecla R após vitória ou derrota.

Não haverá salto, mira manual ou botão de ataque na primeira versão.

### Combate e inimigos

Uma única arma dispara automaticamente contra o inimigo mais próximo dentro de um alcance definido. O ataque começa com intervalo de um segundo e causa um ponto de dano por acerto.

O devoto corrompido será o único tipo inicial de inimigo e perseguirá o jogador. Terá dois pontos de vida e causará um ponto de dano por contato. O projétil será consumido no primeiro acerto e também terá um tempo limite de existência para não permanecer indefinidamente na arena.

Os inimigos surgirão em posições válidas nas bordas da arena, afastadas do jogador. A frequência de surgimento aumentará ao longo da partida. O protótipo terá um limite inicial de 30 inimigos ativos; ao atingir esse limite, novos surgimentos ficam suspensos até haver espaço.

### Pontuação e progressão

Cada inimigo derrotado concede dez pontos e deixa um fragmento de experiência. O fragmento é recolhido quando o jogador encosta nele e vale um ponto de experiência.

A cada cinco fragmentos, o personagem sobe um nível. A melhoria inicial será automática: redução de 10% no intervalo atual entre disparos, respeitando o mínimo de 0,4 segundo. Não haverá tela de escolha de melhorias nesta entrega.

Pontuação e experiência serão valores separados: a primeira registra o resultado da partida, enquanto a segunda determina a evolução da arma.

### Interface e estados da partida

A interface deverá exibir legendas e valores atualizados de pontuação, vida, tempo restante, nível e experiência necessária para o próximo nível. Mensagens curtas informarão início da partida, evolução, vitória e derrota.

Ao vencer ou perder, o surgimento de inimigos e o combate serão interrompidos. A tela exibirá o resultado, a pontuação final e a instrução de reinício. Reiniciar deverá restaurar tempo, vida, pontuação, experiência, arma, inimigos e itens.

Se o dano fatal ocorrer antes ou no mesmo instante em que o tempo terminar, prevalece a derrota; a vitória exige que o jogador ainda tenha vida.

### Efeitos e animação

- Halo nos fragmentos de experiência para facilitar sua identificação.
- Partículas na derrota dos inimigos e ao subir de nível.
- Trail Renderer nos projéteis para destacar sua trajetória.
- Animação por sequência de frames de sprite no efeito de evolução, exibida como elemento da interface.

A animação por frames será um recurso explícito do protótipo. Movimentar modelos 3D, por si só, não atende ao item de animação de sprites solicitado no PDF.

## 3. Fluxo básico da partida

1. A arena carrega com o jogador no centro, vida completa e instruções breves.
2. O cronômetro começa e os primeiros inimigos aparecem nas bordas.
3. O jogador se movimenta; a arma procura alvos e dispara automaticamente.
4. Inimigos derrotados aumentam a pontuação e deixam experiência.
5. O jogador recolhe os fragmentos, sobe de nível e passa a atacar mais rápido.
6. A frequência de surgimento aumenta até o encerramento da partida.
7. O jogo apresenta vitória ou derrota, pontuação final e opção de reinício.

Esse fluxo serve de base para o storyboard. Os esboços visuais do penitente e do devoto corrompido e o storyboard ilustrado ainda precisam ser produzidos caso a Etapa 1 esteja pendente.

## 4. Organização técnica prevista

O projeto será desenvolvido em Unity, com scripts C# e uma cena principal. A versão exata do editor será definida conforme a instalação disponível para a dupla e registrada quando o projeto Unity for criado. A preferência inicial é pelo Built-in Render Pipeline para utilizar o componente Halo sem depender de adaptações de renderização.

As responsabilidades serão divididas entre controle do jogador, comportamento dos inimigos, disparo automático, projéteis, geração de inimigos, experiência, interface e estado da partida. Métodos e eventos comunicarão alterações de vida, pontuação, nível e encerramento, evitando concentrar toda a lógica em um único script.

O uso dos componentes seguirá a função de cada objeto:

| Recurso | Uso previsto |
| --- | --- |
| Transform | Posição, rotação e escala de personagens, cenário e itens |
| Collider | Limites da arena, obstáculos, contato com inimigos e coleta de itens |
| Rigidbody | Movimento e interação física dos personagens, com restrições adequadas ao plano da arena |
| Prefab | Reutilização do jogador, inimigo, projétil, fragmento e efeitos |

A movimentação física deverá respeitar o ciclo de atualização da física. Objetos com Rigidbody não serão reposicionados continuamente pelo Transform durante o combate.

A estrutura futura de assets separará cenas, scripts, prefabs, materiais, modelos, sprites e efeitos. Dependências externas serão mantidas em diretório próprio, com registro de origem e licença. Arquivos temporários e pastas geradas pela Unity não serão versionados.

## 5. Correspondência com a atividade

| Exigência da Aula 11 | Aplicação prevista | Evidência esperada no protótipo |
| --- | --- | --- |
| Cenário funcional e assets 3D — p. 4 | Arena, penitente, devotos corrompidos e obstáculos tridimensionais | Partida executada no cenário |
| Texturas e materiais — p. 5 | Piso de pedra, muros e identificação visual dos objetos | Materiais aplicados aos modelos |
| Configuração de câmera — p. 5 | Câmera elevada que acompanha o jogador | Visibilidade do combate durante a movimentação |
| Transform, Collider e Rigidbody — p. 5 | Posicionamento e física da arena e dos personagens | Componentes configurados e interações funcionando |
| Prefabs — p. 5 | Inimigos, projéteis, itens e efeitos reutilizáveis | Instâncias geradas a partir dos prefabs |
| Halo, partículas e Trail Renderer — p. 5 | Itens luminosos, derrota/evolução e rastro dos disparos | Os três efeitos visíveis em execução |
| Pontuação, legenda e mensagens — p. 6 | HUD com pontuação dinâmica e avisos de estado | Pontos aumentam ao derrotar inimigos |
| Animação de frames de sprites — p. 6 | Efeito de evolução na interface | Sequência de sprites reproduzida ao subir de nível |
| Métodos, funções, eventos, classes e bibliotecas — p. 7 | Scripts C# separados por responsabilidade | Código organizado e eventos ligados à interface |
| Interação entre personagens, objetos e ambiente — p. 7 | Perseguição, dano, coleta e colisões | Ciclo de combate e progressão completo |

## 6. Critérios de conclusão do protótipo

Os itens abaixo representam verificações futuras; nenhum deles foi validado ainda.

- A cena inicia sem erros que impeçam a partida.
- O jogador se move nas quatro direções e nas diagonais, respeitando os limites da arena.
- A câmera acompanha o jogador e mantém a ação visível.
- Inimigos surgem, perseguem e causam dano respeitando o intervalo de invulnerabilidade.
- A arma dispara sem intervenção do jogador, acerta alvos e elimina inimigos.
- Cada inimigo derrotado acrescenta dez pontos uma única vez.
- Fragmentos são recolhidos uma única vez e alimentam a progressão de nível.
- Subir de nível melhora o intervalo de disparo e reproduz a animação de sprites.
- Halo, partículas e rastro dos projéteis aparecem durante a partida.
- Sobreviver por três minutos com vida mostra vitória; perder toda a vida mostra derrota.
- O resultado interrompe o combate, e o reinício restaura todos os valores e objetos da partida.
- A execução é conferida no computador usado para apresentar o trabalho.

## 7. Limites da primeira versão

Ficam fora desta entrega: multiplayer, chefes, várias arenas, geração procedural, inventário, árvore de habilidades, escolha entre várias armas, salvamento de progresso, ranking online, loja, conquistas e versão para celular.

Uma segunda variedade de inimigo, escolhas de melhoria e efeitos sonoros poderão ser avaliados para a versão final somente depois que o protótipo atender aos requisitos obrigatórios. Não são compromissos desta primeira versão.

## 8. Entregas e sequência de execução

O material lista o planejamento em 26/10, o protótipo em 09/11 e a apresentação final em 16/11 para a Turma 1 ou 23/11 para a Turma 2. O PDF não informa o ano; este documento usa 2026 por ser o ano corrente do planejamento.

A entrega de 09/11 corresponde à Etapa 2 da atividade. Elaborar o escopo hoje não conclui essa etapa, que exige uma versão funcional. É necessário confirmar se a documentação da Etapa 1 já foi entregue, pois o PDF exige todas as etapas.

**Entrega atual:** escopo do projeto e repositório com documentação. A implementação será uma etapa posterior.

**Ordem prevista para o desenvolvimento do protótipo:**

1. Criar o projeto Unity e montar arena, câmera, movimento e colisões.
2. Implementar perseguição, geração de inimigos, ataque automático e dano.
3. Integrar pontuação, experiência, evolução, cronômetro e encerramento.
4. Organizar prefabs e aplicar materiais, Halo, partículas, Trail Renderer e animação de sprites.
5. Conferir os critérios de conclusão e preparar a execução para apresentação.

Os requisitos obrigatórios permanecem no escopo. Se houver pouco tempo, a simplificação deverá recair sobre a qualidade dos modelos, o tamanho da arena e a variedade visual, preservando o ciclo jogável e os itens solicitados.

**Entrega final:** corrigir os problemas observados no protótipo, ajustar a dificuldade e preparar um vídeo de até um minuto mostrando cenário, efeitos e lógica em execução. A apresentação presencial terá até 15 minutos por grupo e deverá abordar as dificuldades reais encontradas e as soluções adotadas.

## 9. Pendências e cuidados de execução

- Confirmar se a dupla pertence à Turma 1 ou à Turma 2 para fixar a data final.
- Confirmar a situação da Etapa 1 e produzir os esboços e o storyboard, se necessário.
- Definir a versão da Unity e o computador da apresentação antes de começar a implementação.
- Dividir as tarefas entre Miguel e Daniel conforme a disponibilidade de cada um.
- Conferir a licença de qualquer asset externo antes de incluí-lo no repositório.
- Verificar cedo o desempenho com 30 inimigos ativos e ajustar o ritmo de surgimento se necessário.

## 10. Avaliação

Segundo a página 11 do material, o projeto vale oito pontos: criatividade e originalidade (2,5), aplicação dos conceitos (2,5), documentação e organização (2,0), e apresentação e clareza do grupo (1,0). Os outros dois pontos da P02 correspondem à entrega das Aulas 09 e 10, que não faz parte deste escopo.

A prioridade é concluir um ciclo de sobrevivência compreensível, demonstrar os conceitos pedidos e manter o projeto organizado. A identidade da catedral e dos penitentes orientará a apresentação visual sem aumentar a quantidade de sistemas necessários.
