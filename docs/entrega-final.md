# Preparação para 16/11

A nova cena Catedral é gerada sem substituir a cena Claustro. Em um projeto já aberto, usar Maçonaria > Preparar cena inicial. Os prefabs são compartilhados: manter cópia da versão anterior antes de importar.

Mudanças implementadas: arena de 76 metros com alas de colunas, sepulturas, velas, jardim e santuário; textura de pedra repetida em escala maior; efeitos reduzidos; detalhes de vestes e animação procedural dos braços; três armas com cinco níveis independentes e escolha por teclas 1, 2 e 3 ao evoluir. A escolha pausa a partida. Relicário dispara projéteis, incensário atinge uma área e lâminas causam dano em posições orbitais.

Revisão de textura: a versão antiga preservava cenas existentes, o que pode manter materiais de uma geração anterior. O shader continua Standard, para Built-in. Não há evidência suficiente para atribuir a falha observada ao Linux. Conferir textura de pedra no material e renderização da cena Catedral no editor e no executável.

Pendências: execução e compilação no Unity; perfil de desempenho; teste de todas as combinações de armas; acabamento dos modelos além das formas geométricas; animações de ataque, dano e morte; melhoria visual do incensário. A movimentação inimiga ainda é perseguição direta e deve ser testada ao redor de obstáculos. Não há assets externos adicionados.

A revisão de código e o diff não substituem uma partida completa. Esta versão ainda não foi validada para apresentação.

## Atualização de 16/11

Versão de referência: Unity 2022.3.62f3 LTS, Built-in Render Pipeline. A atualização introduz materiais com texturas procedurais persistidas, verificação de pipeline, vestes de malha com pregas, reação de dano/morte, desvio local de obstáculos, ajuste das armas e efeitos com fade em tempo não escalado. Coleta usa permanência no trigger para permitir retomada após a escolha. Lâminas melhoram a cadência em todos os níveis.

Em cenas já geradas, usar Maçonaria > Preparar cena inicial para atualizar os recursos dos prefabs ou os menus Atualizar materiais e texturas e Atualizar vestes dos personagens. Preserve alterações manuais antes da atualização. O gerador não reconstrói a arquitetura de uma Catedral existente.

A textura agora tem cor própria, e o material usa branco para evitar escurecimento duplicado. Importação define repetição, mipmaps e filtragem. Isso corrige limitações da configuração anterior, mas não comprova a causa do problema relatado no Linux.

Verificações realizadas nesta atualização: revisão estática e git diff --check. Nenhuma compilação, partida completa ou medição de desempenho foi executada no ambiente de desenvolvimento, que não possui Unity. O comportamento no build e a apresentação visual permanecem pendentes de validação. A arte ainda é procedural; o acabamento de personagens, arquitetura e animações deve ser avaliado no editor antes de ser considerado final. Desvio local não equivale a navegação completa.
