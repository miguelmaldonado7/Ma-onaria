# Validação do protótipo — 09/11/2026

Status: pendente de execução no Unity 2022.3.62f3. Esta lista não representa testes aprovados.

- [ ] Importar o projeto sem erros no Console.
- [ ] Confirmar geração da cena, textura, materiais e cinco prefabs.
- [ ] Mover por WASD e setas, inclusive na diagonal, sem atravessar muros e altar.
- [ ] Conferir perseguição, limite de 30 inimigos e aumento da frequência de surgimento.
- [ ] Confirmar ataque automático e dois acertos para derrotar um devoto.
- [ ] Confirmar dez pontos e um fragmento por inimigo derrotado.
- [ ] Recolher cinco fragmentos e verificar nível, redução do intervalo e animação por frames.
- [ ] Conferir partículas, Trail Renderer e Halo nos fragmentos. A luz pontual não substitui a verificação do Halo.
- [ ] Receber dano e conferir o intervalo de um segundo antes do próximo dano.
- [ ] Perder três pontos de vida: resultado de derrota e combate parado.
- [ ] Sobreviver três minutos: resultado de vitória e combate parado.
- [ ] Reiniciar após ambos os resultados e verificar restauração de vida, tempo, pontos, nível e arena.
- [ ] Gerar o executável e repetir uma partida no computador da apresentação.

A versão usa modelos geométricos e não inclui áudio, chefes, escolha de armas ou persistência. Os esboços e o storyboard da Etapa 1 continuam separados desta implementação. Para a apresentação final, registrar as dificuldades efetivamente observadas e gravar vídeo de até um minuto.

## Revisão do código antes do teste na faculdade

Corrigidos o módulo de conversão de imagens usado por `EncodeToPNG`, o módulo de renderização de texto e a referência à fonte interna `LegacyRuntime.ttf`. O movimento agora lê WASD e setas diretamente, sem depender dos eixos configurados no Input Manager. A preparação aguarda a importação, reaproveita materiais de uma tentativa interrompida e restaura a inclusão da cena nas configurações de build quando ela já existe.

Verificação realizada: revisão dos scripts, validação do manifesto JSON e conferência do diff. Compilação, física, efeitos e execução continuam pendentes no Unity. Os logs recebidos do Mac mostram falha de inicialização também em um projeto vazio; essas correções não são apresentadas como solução confirmada para essa falha do editor.

Na faculdade, usar uma cópia atualizada do repositório, Unity 2022.3.62f3 e renderização Built-in. Se a janela Game não responder ao teclado, clicar dentro dela antes de movimentar. Conferir o Console antes de iniciar a apresentação.
