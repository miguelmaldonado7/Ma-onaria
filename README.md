# Maçonaria

Protótipo de sobrevivência em arena 3D, com ataques automáticos e ambientação de um claustro em ruínas. Desenvolvido para a disciplina de Desenvolvimento de Jogos Digitais por Miguel Maldonado Ferreira e Daniel Arlindo.

## Abrir

1. Instale o Unity **2022.3.62f3 LTS** pelo Unity Hub, com suporte a Windows Build Support se for gerar o executável Windows.
2. No Hub, escolha **Add project from disk** e selecione esta pasta.
3. Aguarde a importação e a compilação. A preparação inicial cria a cena `Assets/Scenes/Claustro.unity`, os materiais, a textura de pedra e os prefabs.
4. Caso a cena não abra automaticamente, use **Maçonaria → Preparar cena inicial**.
5. Pressione **Play**.

A preparação não substitui uma cena já existente. Depois da primeira importação, salve e versione a cena e os assets gerados com seus arquivos `.meta`.

## Jogar

WASD ou setas movimentam o penitente. O relicário ataca automaticamente o inimigo mais próximo em alcance. Cada devoto derrotado vale dez pontos e deixa um fragmento de experiência. Cinco fragmentos aumentam o nível e aceleram os disparos.

Sobreviva por três minutos. Há três pontos de vida e um segundo de proteção após cada dano. Pressione **R** na tela de resultado para reiniciar.

## Conteúdo

- Arena 3D com altar, pilares, arcos partidos e lápides.
- Personagem com física, câmera elevada e inimigos perseguidores.
- Ataque automático, progressão de nível, pontuação e cronômetro.
- Prefabs, materiais, textura procedural, partículas e rastro de projéteis.
- Halo preparado nos fragmentos quando o componente interno estiver disponível no editor; luz pontual complementar.
- Animação de oito frames de sprite ao subir de nível.
- Vitória, derrota e reinício da partida.

Os modelos são geométricos e os recursos visuais são produzidos pelo projeto. A jogabilidade tem Vampire Survivors como referência; a ambientação tem Blasphemous como referência.

## Gerar executável

Abra **File → Build Settings**, selecione **PC, Mac & Linux Standalone / Windows / x86_64** e clique em **Build**. A cena é adicionada à lista durante a preparação inicial. Distribua a pasta completa gerada, não somente o `.exe`.

Também é possível executar o editor pela linha de comando:

```sh
Unity -batchmode -quit -projectPath /caminho/Ma-onaria -executeMethod PrepareArena.BuildWindows -logFile build.log
```

## Verificação

O projeto foi preparado sem acesso ao editor Unity no ambiente de desenvolvimento. **Não há compilação, teste de execução ou executável validado nesta entrega.** A abertura no editor e a conferência do [roteiro de validação](docs/validacao.md) são pendências antes da apresentação. O Halo, em especial, precisa ser conferido no Unity 2022.3.

Consulte o [escopo](docs/escopo.md) para os requisitos e limites da primeira versão.
